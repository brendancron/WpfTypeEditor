using System.ComponentModel;

namespace Editor;

public interface IValueViewModel : INotifyPropertyChanged
{
   object? UntypedValue { get; set; }
}

public interface IValueViewModel<T> : IValueViewModel
{

   T? Value { get; set; }

}
