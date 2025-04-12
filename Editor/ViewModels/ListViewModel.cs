using Editor.Factories;
using System.Collections.ObjectModel;

namespace Editor.ViewModels;

public interface IGenericListAdder
{
   void AddDefaultItem();
}

[RegisterViewModel(typeof(List<>))]
public class ListViewModel<T> : ValueViewModel<List<T>>, IGenericListAdder
{
   public ObservableCollection<IValueViewModel> Items { get; }

   public ListViewModel(Func<List<T>?> getter, Action<List<T>?> setter) : base(getter, setter)
   {
      Items = new ObservableCollection<IValueViewModel>(
         (Value ?? new List<T>()).Select(CreateItemViewModel));

      Items.CollectionChanged += (_, __) =>
      {
         SyncValueFromItems();
      };
   }

   public void SyncValueFromItems()
   {
      var list = new List<T>();
      foreach (var item in Items)
      {
         if (item is IValueViewModel<T> vm)
         {
            list.Add(vm.Value);
         }
      }
      Value = list;
   }

   private IValueViewModel CreateItemViewModel(T value)
   {
      T? backingValue = value;

      var getter = (Func<T?>)(() => backingValue);

      var setter = (T? newValue) =>
      {
         if (!EqualityComparer<T?>.Default.Equals(backingValue, newValue))
         {
            backingValue = newValue;
            SyncValueFromItems();
         }
      };

      var vm = ValueViewModelFactory.Create(
         typeof(T),
         getter,
         setter);

      return vm;
   }

   public void AddItem(T value)
   {
      Items.Add(CreateItemViewModel(value));
   }

   public void AddDefaultItem()
   {
      AddItem(default!);
   }
}