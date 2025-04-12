namespace Editor.Extensions;

public static class TypeExtension
{

   public static Type MakeGenericTypeIfNeeded(this Type viewModelType, Type inputType)
   {
      if (viewModelType.IsGenericTypeDefinition)
      {
         return viewModelType.MakeGenericType(new[] { inputType });
      }

      return viewModelType;
   }


}
