namespace Editor.ViewModels;

public class UnsupportedTypeViewModel<T> : ValueViewModel<T>
{
   public string Message { get; }

   public UnsupportedTypeViewModel() : base(() => default!, (T t) => { })
   {
      Message = $"Unsupported Type: {typeof(T).FullName}";
   }
}

public class UnsupportedTypeViewModel : ValueViewModel<object>
{
   public string Message { get; }

   public UnsupportedTypeViewModel(Type type) : base(() => default!, (object obj) => { })
   {
      Message = $"Unsupported Type: {type.FullName}";
   }
}