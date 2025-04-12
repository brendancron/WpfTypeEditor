using Editor.Factories;

namespace Editor.ViewModels;

[RegisterViewModel(typeof(bool))]
public class BoolViewModel : ValueViewModel<bool>
{

   public BoolViewModel(Func<bool> getter, Action<bool> setter) : base(getter, setter) { }

}