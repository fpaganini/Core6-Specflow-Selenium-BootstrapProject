using System.IO;
using SpecflowProject.Drivers;
using OpenQA.Selenium;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Infrastructure;

namespace SpecflowProject.Hooks
{
    [Binding]
    public class LoggingHook
    {
        private readonly BrowserDriver _browserDriver;
        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;

        public LoggingHook(BrowserDriver browserDriver, ISpecFlowOutputHelper specFlowOutputHelper)
        {
            _browserDriver = browserDriver;
            _specFlowOutputHelper = specFlowOutputHelper;
        }

        [AfterStep()]
        public void TakeScreenshotAfterEachStep()
        {
            if (ConfigHook.Settings?.SystemUnderTest?.PrintScreenEachStep.GetValueOrDefault() == true &&
                _browserDriver.WebDriver is ITakesScreenshot screenshotTaker)
            {
                var filename = Path.ChangeExtension(Path.GetRandomFileName(), "png");

                screenshotTaker.GetScreenshot().SaveAsFile(filename);

                _specFlowOutputHelper.AddAttachment(filename);
            }
        }

        [AfterScenario(Order = 1)]
        public void HandleWebErrors(ScenarioContext _scenarioContext)
        {
            if (ConfigHook.Settings?.SystemUnderTest?.PrintScreenOnError.GetValueOrDefault() == true &&
                _scenarioContext?.TestError != null &&
                _browserDriver.WebDriver is ITakesScreenshot screenshotTaker)
            {
                var filename = Path.ChangeExtension($"Erro_{Path.GetRandomFileName()}", "png");

                screenshotTaker.GetScreenshot().SaveAsFile(filename);

                _specFlowOutputHelper.AddAttachment(filename);
            }
        }
    }
}