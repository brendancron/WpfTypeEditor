using Editor.Extensions;
using Editor.ViewModels;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace Editor.Factories;

public static class ValueViewModelFactory
{
   private static Dictionary<Type, Type> registrations = new();

   public static void Register(Type targetType, Type viewModelType)
   {
      registrations.Add(targetType, viewModelType);
   }

   public static IValueViewModel<T> Create<T>(Func<T?> getter, Action<T?> setter)
   {
      if (TryGetVMType(typeof(T), out var viewModelType))
      {
         return (IValueViewModel<T>)Activator.CreateInstance(viewModelType, getter, setter)!;
      }

      return new UnsupportedTypeViewModel<T>();
   }

   public static IValueViewModel Create(Type targetType, object getter, object setter)
   {
      if (TryGetVMType(targetType, out var viewModelType))
      {
         return (IValueViewModel)Activator.CreateInstance(viewModelType, getter, setter)!;
      }

      return new UnsupportedTypeViewModel(targetType);
   }


   public static bool TryGetVMType(Type targetType, [NotNullWhen(true)] out Type? vmType)
   {
      vmType = null;

      if (targetType.IsGenericType)
      {
         var genericDef = targetType.GetGenericTypeDefinition();
         if (registrations.TryGetValue(genericDef, out var vmGenericType))
         {
            vmType = vmGenericType.MakeGenericType(targetType.GenericTypeArguments);
            return true;
         }
      }

      if (registrations.TryGetValue(targetType, out vmType))
      {
         return true;
      }

      foreach (var entry in registrations)
      {
         if (entry.Key.IsAssignableFrom(targetType))
         {
            vmType = entry.Value.MakeGenericTypeIfNeeded(targetType);
            return true;
         }
      }

      return false;
   }

   public static void AutoRegisterAll()
   {
      var types = AppDomain.CurrentDomain.GetAssemblies()
          .Where(a => !a.IsDynamic && a.GetName().Name != "Microsoft.GeneratedCode")
          .SelectMany(a =>
          {
             try { return a.GetTypes(); }
             catch { return Array.Empty<Type>(); }
          })
          .Where(t => typeof(IValueViewModel).IsAssignableFrom(t) && !t.IsAbstract);

      foreach (var type in types)
      {
         var attributes = type.GetCustomAttributes<RegisterViewModelAttribute>();

         foreach (var attr in attributes)
         {
            Register(attr.MatchType, type);
         }
      }
   }

}
