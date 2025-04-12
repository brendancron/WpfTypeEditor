using Editor.Extensions;
using Editor.Factories;

namespace Editor.ViewModels;

[RegisterViewModel(typeof(Enum))]
public class EnumViewModel<TEnum> : BaseValueViewModel<TEnum> where TEnum : Enum
{
   public List<EnumOption<TEnum>> Values { get; }

   private readonly Func<TEnum?> _getter;
   private readonly Action<TEnum?> _setter;

   public EnumViewModel(Func<TEnum?> getter, Action<TEnum?> setter)
   {
      _getter = getter;
      _setter = setter;

      Values = Enum.GetValues(typeof(TEnum))
          .Cast<TEnum>()
          .Select(v => new EnumOption<TEnum> { Value = v, Display = v.GetDescription() })
          .ToList();
   }

   public override TEnum? Value
   {
      get => _getter();
      set
      {
         _setter(value);
         OnPropertyChanged();
         OnValueChanged(value);
      }
   }
}

public class EnumOption<T>
{
   public required T Value { get; init; }
   public required string Display { get; init; }
}
