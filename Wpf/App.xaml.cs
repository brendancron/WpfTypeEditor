using Editor;
using Editor.Factories;
using System.Windows;

namespace Wpf;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : Application
{

   public App()
   {
      ValueViewModelFactory.AutoRegisterAll();
   }

}

