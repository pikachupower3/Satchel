using System.Reflection;

namespace Satchel.BetterMenus.Attributes;

/// <summary>
/// Add this attribute on a method to create a <see cref="MenuButton"/> that when pressed will invoke the function
/// Requires the method this attribute is on to be an instance method with no parameters
/// </summary>
public class MenuButtonAttribute : ElementAttribute
{
    public string Description;
    
    /// <inheritdoc cref="MenuButtonAttribute"/>
    /// <param name="name">The name of the element to show in the menu. Also is the id</param>
    /// <param name="description">The description of the element</param>
    public MenuButtonAttribute(string name, string description) : base(name)
    {
        Description = description;
    }

    public override bool VerifyCorrectFieldType(MemberInfo memberInfo) => memberInfo is MethodInfo methodInfo && 
                                                                          methodInfo.GetParameters().Length == 0 && 
                                                                          !methodInfo.IsGenericMethod && 
                                                                          !methodInfo.IsStatic;

    public override Element[] CreateElement<Settings>(MemberInfo memberInfo, Settings settings)
    {
        var methodInfo = memberInfo as MethodInfo;
        return new Element[]
        {
            new MenuButton(Name,
                Description, 
                _ => methodInfo!.Invoke(settings, null)),
        };
    }
}
