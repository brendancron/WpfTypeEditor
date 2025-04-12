using Editor;
using Editor.Attributes;

namespace Wpf;

public class Roster
{

   [Export]
   public List<string> names { get; set; }

   [Export]
   public City city { get; set; }

}
