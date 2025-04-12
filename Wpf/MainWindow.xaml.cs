using Editor.ViewModels;
using System.Windows;

namespace Wpf;

public partial class MainWindow : Window
{
   public MainWindow()
   {
      InitializeComponent();

      var person = new Person
      {
         FirstName = "Edgar",
         LastName = "Poe"
      };

      DataContext = new ObjectViewModel<Person>(person);
   }
}
