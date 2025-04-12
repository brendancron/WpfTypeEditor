using Editor.Factories;

namespace Editor.ViewModels;

[RegisterViewModel(typeof(int))]
public class IntViewModel : ValueViewModel<int>
{
   public IntViewModel(Func<int> getter, Action<int> setter)
        : base(getter, setter) { }

   private string _displayString;
   public string DisplayString
   {
      get => _displayString;
      set
      {
         if (_displayString != value)
         {
            _displayString = value;
            if (int.TryParse(value, out int result))
            {
               Value = result;
               OnPropertyChanged(nameof(Value));
            }
            OnPropertyChanged();
         }
      }
   }

}
