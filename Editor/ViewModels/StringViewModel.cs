using Editor.Factories;

namespace Editor.ViewModels;

[RegisterViewModel(typeof(string))]
public class StringViewModel : ValueViewModel<string>
{
   public StringViewModel(Func<string?> getter, Action<string?> setter)
        : base(getter, setter) { }

}
