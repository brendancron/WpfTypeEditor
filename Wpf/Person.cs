using Editor.Attributes;
using System.Text.Json;
using System.Windows;

namespace Wpf;

public class Person
{
   [Export]
   public string FirstName { get; set; }

   [Export]
   public string LastName { get; set; }

   [Export]
   public int Age { get; set; }

   [Export]
   public List<List<int>> nestedList{ get; set; }
   
   [Export]
   public bool IsMale { get; set; }

   [Export]
   public List<string> Hobbies { get; set; } = new List<string>();

   [Export]
   public void Print()
   {
      string json = JsonSerializer.Serialize(this, new JsonSerializerOptions { WriteIndented = true });
      MessageBox.Show(json);
   }

}