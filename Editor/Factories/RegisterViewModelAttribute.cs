namespace Editor.Factories;

[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
public class RegisterViewModelAttribute : Attribute
{
   public Type MatchType { get; }

   public RegisterViewModelAttribute(Type matchType)
   {
      MatchType = matchType;
   }
}
