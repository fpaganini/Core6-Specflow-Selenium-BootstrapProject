using SpecflowProject.Drivers;

namespace SpecflowProject.Steps;

[Binding]
public sealed class TemplateStepDefinitions
{
    private readonly TemplatePageDriver _templatePageDriver;

    public TemplateStepDefinitions(BrowserDriver browserDriver)
        => _templatePageDriver = new TemplatePageDriver(browserDriver.WebDriver);

    [Dado("o primeiro número é (.*)")]
    public void GivenTheFirstNumberIs(int number)
        => _templatePageDriver.EnterFirstNumber(number);

    [Dado("o segundo número é (.*)")]
    public void GivenTheSecondNumberIs(int number)
        => _templatePageDriver.EnterSecondNumber(number);

    [Quando("os dois números são adicionados")]
    public void WhenTheTwoNumbersAreAdded()
        => _templatePageDriver.ClickAdd();

    [Entao("o resultado deve ser (.*)")]
    public void ThenTheResultShouldBe(int expectedResult)
        => _templatePageDriver.CheckResult(expectedResult);
}
