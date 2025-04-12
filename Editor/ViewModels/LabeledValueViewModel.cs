namespace Editor.ViewModels;

public class LabeledValueViewModel
{
   public string Label { get; }
   public IValueViewModel Value { get; }

   public LabeledValueViewModel(string label, IValueViewModel value)
   {
      Label = label;
      Value = value;
   }
}