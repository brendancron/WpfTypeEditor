using System.ComponentModel;
using System.Reflection;

namespace Editor.Extensions;

public static class EnumExtensions
{
   public static string GetDescription(this Enum value)
   {
      return value.GetType()
          .GetField(value.ToString())?
          .GetCustomAttribute<DescriptionAttribute>()?
          .Description
          ?? value.ToString();
   }
}
