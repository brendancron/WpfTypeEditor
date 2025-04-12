using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Editor;

public abstract class BaseValueViewModel<T> : IValueViewModel<T>
{
   public abstract T? Value { get; set; }

   public Type ValueType => typeof(T);

   public object? UntypedValue
   {
      get => Value;
      set => Value = (T?)value;
   }

   public event PropertyChangedEventHandler? PropertyChanged;
   public event Action<T?>? ValueChanged;

   protected void OnPropertyChanged([CallerMemberName] string? propertyName = null) =>
       PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));

   protected void OnValueChanged(T? value) =>
        ValueChanged?.Invoke(value);
}

