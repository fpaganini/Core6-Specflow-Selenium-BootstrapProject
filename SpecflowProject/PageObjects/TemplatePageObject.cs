using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SpecflowProject.Hooks;

namespace SpecflowProject.PageObjects;

public class TemplatePageObject
{
    private string FinalUrl = $"{ConfigHook.Settings?.SystemUnderTest?.BaseURL}/Calculator.html";
    private int DefaultWaitInSeconds = 5;

    private readonly IWebDriver _webDriver;
    private readonly Lazy<IWebElement> _firstNumberElement;
    private readonly Lazy<IWebElement> _secondNumberElement;
    private readonly Lazy<IWebElement> _addButtonElement;
    private readonly Lazy<IWebElement> _resultElement;
    private readonly Lazy<IWebElement> _resetButtonElement;


    public TemplatePageObject(IWebDriver webDriver)
    {
        _webDriver = webDriver;
        _firstNumberElement = new Lazy<IWebElement>(() => _webDriver.FindElement("#first-number"));
        _secondNumberElement = new Lazy<IWebElement>(() => _webDriver.FindElement("#second-number"));
        _addButtonElement = new Lazy<IWebElement>(() => _webDriver.FindElement("#add-button"));
        _resultElement = new Lazy<IWebElement>(() => _webDriver.FindElement("#result"));
        _resetButtonElement = new Lazy<IWebElement>(() => _webDriver.FindElement("#reset-button"));
    }

    private IWebElement FirstNumberElement => _firstNumberElement.Value;
    private IWebElement SecondNumberElement => _secondNumberElement.Value;
    private IWebElement AddButtonElement => _addButtonElement.Value;
    private IWebElement ResultElement => _resultElement.Value;
    private IWebElement ResetButtonElement => _resetButtonElement.Value;

    public void EnterFirstNumber(string number)
    {
        FirstNumberElement.Clear();
        FirstNumberElement.SendKeys(number);
    }

    public void EnterSecondNumber(string number)
    {
        SecondNumberElement.Clear();
        SecondNumberElement.SendKeys(number);
    }

    public void ClickAdd()
        => AddButtonElement.Click();

    public void EnsureIsOpenAndReset()
    {
        if (_webDriver.Url != FinalUrl)
            _webDriver.Url = FinalUrl;
        else
        {
            ResetButtonElement.Click();
            WaitForEmptyResult();
        }
    }

    public string WaitForNonEmptyResult()
    {
        return WaitUntil(
            () => ResultElement.GetValue(),
            result => !result.IsNullOrEmpty());
    }

    public string WaitForEmptyResult()
    {
        //Wait for the result to be empty
        return WaitUntil(
            () => ResultElement.GetValue(),
            result => result.IsEmpty());
    }

    private T WaitUntil<T>(Func<T> getResult, Func<T, bool> isResultAccepted) where T : class
    {
        return _webDriver.Wait(DefaultWaitInSeconds).Until(driver =>
        {
            var result = getResult();
            if (!isResultAccepted(result))
                return default;

            return result;
        });

    }
}