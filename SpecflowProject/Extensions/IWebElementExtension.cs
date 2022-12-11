using OpenQA.Selenium;

namespace SpecflowProject.Extensions;

public static class IWebElementExtension
{
    public static string GetValue(this IWebElement original)
        => original.GetAttribute("value");
}
