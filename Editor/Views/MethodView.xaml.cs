using Editor.ViewModels;
using System.Windows;
using System.Windows.Controls;

namespace Editor.Views;

public partial class MethodView : UserControl
{
   public MethodView()
   {
      InitializeComponent();
   }

   private void OnClick(object sender, RoutedEventArgs e)
   {
      if (DataContext is IMethodViewModel vm)
      {
         vm.Invoke();
      }
   }
}
