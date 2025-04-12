using Editor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Editor.Views;

public partial class ListView : UserControl
{
   public ListView()
   {
      InitializeComponent();
   }

   private void OnAddItemClicked(object sender, RoutedEventArgs e)
   {
      if (DataContext is IValueViewModel vm &&
          vm is IGenericListAdder listAdder)
      {
         listAdder.AddDefaultItem();
      }
   }
}