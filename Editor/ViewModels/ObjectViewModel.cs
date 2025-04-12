using Editor.Attributes;
using Editor.Extensions;
using Editor.Factories;
using System.Reflection;

namespace Editor.ViewModels;

public class ObjectViewModel<T> : BaseValueViewModel<T>
{
   public IReadOnlyList<LabeledValueViewModel> Fields { get; }

   private readonly T _target;

   public ObjectViewModel(T target)
   {
      _target = target;

      Fields = typeof(T)
         .GetMembers(BindingFlags.Public | BindingFlags.Instance)
         .Where(m => m is PropertyInfo or FieldInfo or MethodInfo)
         .Where(m => m.GetCustomAttribute<ExportAttribute>() != null)
         .Select(member =>
         {
            var fieldType = member.GetMemberType();
            var getMethod = typeof(MemberInfoExtension)
                  .GetMethod(nameof(MemberInfoExtension.MakeGetter))!
                  .MakeGenericMethod(typeof(T), fieldType);

            var setMethod = typeof(MemberInfoExtension)
                  .GetMethod(nameof(MemberInfoExtension.MakeSetter))!
                  .MakeGenericMethod(typeof(T), fieldType);

            var label = member.Name;
            var getter = getMethod.Invoke(null, [member, target])!;
            var setter = setMethod.Invoke(null, [member, target])!;

            var valueVm = ValueViewModelFactory.Create(fieldType, getter, setter);
            var labeledVm = new LabeledValueViewModel(label, valueVm);
            return labeledVm;
         })
         .ToList();
   }

   public override T? Value
   {
      get => _target;
      set { /* you could apply to a backing field here if needed */ }
   }
}