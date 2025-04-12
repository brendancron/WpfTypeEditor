using System.Windows.Controls;
using System.Windows;

namespace Editor;

public class ConventionViewTemplateSelector : DataTemplateSelector
{
   public override DataTemplate? SelectTemplate(object item, DependencyObject container)
   {
      if (item == null) return null;

      var viewModelType = item.GetType();
      string viewModelTypeName = viewModelType.FullName!;
      
      if (viewModelType.IsGenericType)
      {
         var genericDef = viewModelType.GetGenericTypeDefinition();
         var fullName = genericDef.FullName!;
         viewModelTypeName = fullName[..fullName.IndexOf('`')];
      }

      var viewTypeName = viewModelTypeName
          .Replace("ViewModels", "Views")
          .Replace("ViewModel", "View");

      var viewType = Type.GetType(viewTypeName);
      if (viewType == null) return null;

      // Create a DataTemplate that renders the view
      var factory = new FrameworkElementFactory(viewType);
      factory.SetValue(FrameworkElement.DataContextProperty, item);

      var template = new DataTemplate
      {
         VisualTree = factory
      };

      return template;
   }
}

