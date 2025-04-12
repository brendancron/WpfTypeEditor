using Editor.Factories;

namespace Editor.ViewModels;

public interface IMethodViewModel
{
   void Invoke();
}


[RegisterViewModel(typeof(Delegate))]
public class MethodViewModel<TDelegate> : BaseValueViewModel<TDelegate>, IMethodViewModel where TDelegate : Delegate
{
   private readonly Func<TDelegate?> _getter;

   public MethodViewModel(Func<TDelegate?> getter, Action<TDelegate?> setter)
   {
      _getter = getter;
   }

   public override TDelegate? Value
   {
      get => _getter();
      set { /* optional: no-op */ }
   }

   public void Invoke()
   {
      Value?.DynamicInvoke();
   }
}
