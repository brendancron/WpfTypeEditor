using System.Linq.Expressions;
using System.Reflection;

namespace Editor.Extensions;

public static class MemberInfoExtension
{

   public static Func<TField?> MakeGetter<T, TField>(this MemberInfo member, T target)
   {
      if (member is PropertyInfo pi)
      {
         var targetExpr = Expression.Constant(target);
         var access = Expression.Property(targetExpr, pi);
         var convert = Expression.Convert(access, typeof(TField));
         return Expression.Lambda<Func<TField?>>(convert).Compile();
      }

      if (member is FieldInfo fi)
      {
         var targetExpr = Expression.Constant(target);
         var access = Expression.Field(targetExpr, fi);
         var convert = Expression.Convert(access, typeof(TField));
         return Expression.Lambda<Func<TField?>>(convert).Compile();
      }

      if (member is MethodInfo mi)
      {
         var delegateType = mi.GetDelegateType();

         if (!typeof(TField).IsAssignableFrom(delegateType))
         {
            throw new InvalidCastException($"Cannot assign method delegate {delegateType} to {typeof(TField)}.");
         }

         var del = Delegate.CreateDelegate(delegateType, target, mi);
         return () => (TField)(object)del!;
      }

      throw new NotSupportedException($"Unsupported member type: {member.GetType()}");
   }

   public static Action<TField?> MakeSetter<T, TField>(this MemberInfo member, T target)
   {
      if (member is PropertyInfo pi && pi.CanWrite)
      {
         var targetExpr = Expression.Constant(target);
         var param = Expression.Parameter(typeof(TField), "v");
         var assign = Expression.Assign(Expression.Property(targetExpr, pi), param);
         var lambda = Expression.Lambda<Action<TField?>>(assign, param);
         return lambda.Compile();
      }

      if (member is FieldInfo fi)
      {
         var targetExpr = Expression.Constant(target);
         var param = Expression.Parameter(typeof(TField), "v");
         var assign = Expression.Assign(Expression.Field(targetExpr, fi), param);
         var lambda = Expression.Lambda<Action<TField?>>(assign, param);
         return lambda.Compile();
      }

      // For methods, return a no-op setter
      return _ => { };
   }

   public static Type GetMemberType(this MemberInfo memberInfo)
   {
      return memberInfo switch
      {
         PropertyInfo propertyInfo => propertyInfo.PropertyType,
         FieldInfo fieldInfo => fieldInfo.FieldType,
         MethodInfo methodInfo => methodInfo.GetDelegateType(),
         _ => throw new NotSupportedException($"Member type {memberInfo.MemberType} is not supported.")
      };
   }

   public static Type GetDelegateType(this MethodInfo methodInfo)
   {
      var parameters = methodInfo.GetParameters()
                                 .Select(p => p.ParameterType)
                                 .ToList();

      if (methodInfo.ReturnType == typeof(void))
      {
         return Expression.GetActionType(parameters.ToArray());
      }
      else
      {
         parameters.Add(methodInfo.ReturnType);
         return Expression.GetFuncType(parameters.ToArray());
      }
   }

   public static bool HasAttribute<T>(this MemberInfo memberInfo)
      where T : Attribute
   {
      return memberInfo.GetCustomAttribute<T>() != null;
   }

}
