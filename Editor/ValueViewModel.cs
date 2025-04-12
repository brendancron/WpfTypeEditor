using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Editor;

public abstract class ValueViewModel<T> : BaseValueViewModel<T>
{
   private readonly Func<T?> _getter;
   private readonly Action<T?> _setter;

   public ValueViewModel(Func<T?> getter, Action<T?> setter)
   {
      _getter = getter;
      _setter = setter;
   }

   public override T? Value
   {
      get => _getter();
      set
      {
         if (!EqualityComparer<T?>.Default.Equals(_getter(), value))
         {
            _setter(value);
            OnPropertyChanged();
            OnValueChanged(value);
         }
      }
   }
}