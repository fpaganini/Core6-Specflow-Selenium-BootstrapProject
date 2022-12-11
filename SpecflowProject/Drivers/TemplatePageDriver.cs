using OpenQA.Selenium;
using SpecflowProject.PageObjects;

namespace SpecflowProject.Drivers;

public class TemplatePageDriver
{
    private readonly TemplatePageObject _pageObject;

    public TemplatePageDriver(IWebDriver webDriver)
        => _pageObject = new TemplatePageObject(webDriver);

    public void EnterFirstNumber(int number)
        => _pageObject.EnterFirstNumber(number.ToString());

    public void EnterSecondNumber(int number)
        => _pageObject.EnterSecondNumber(number.ToString());

    public void ClickAdd()
        => _pageObject.ClickAdd();

    public void CheckResult(int expectedResult)
    {
        var actualResult = _pageObject.WaitForNonEmptyResult();
        actualResult.Should().Be(expectedResult.ToString());
    }
}
