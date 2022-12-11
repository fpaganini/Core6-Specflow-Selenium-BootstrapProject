using SpecflowProject.Drivers;
using SpecflowProject.PageObjects;

namespace SpecflowProject.Hooks;

[Binding]
public class TemplateHook
{
    [BeforeScenario("Template")]
    public static void BeforeScenario(BrowserDriver browserDriver)
    {
        var pageObject = new TemplatePageObject(browserDriver.WebDriver);
        pageObject.EnsureIsOpenAndReset();
    }
}