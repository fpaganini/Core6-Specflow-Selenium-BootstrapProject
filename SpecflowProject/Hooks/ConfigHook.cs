using BoDi;
using Microsoft.Extensions.Configuration;
using SpecFlow.Actions.Selenium;
using SpecflowProject.Support;

namespace SpecflowProject.Hooks;

[Binding]
public class ConfigHook
{
    public static IConfigurationRoot? Configuration;
    public static AppSettingsModel? Settings;
    public static Drivers.BrowserDriver? BrowserDriver;

    public static IConfigurationRoot GenerateConfiguration(string env)
    {
        var configBuilder = new ConfigurationBuilder();

        var appSettingsFileName = string.IsNullOrEmpty(env)
                    ? "appsettings.json" : $"appsettings.{env}.json";

        var configurationRoot = configBuilder
            .AddEnvironmentVariables()
            .AddJsonFile(appSettingsFileName, optional: false, reloadOnChange: false)
            .Build();

        Settings = AppSettingsModel.Bind(configurationRoot);

        return configurationRoot;
    }

    [BeforeScenario(Order = -10)]
    public static void BeforeTestRunning(ObjectContainer testThreadContainer)
    {
        string env = "Development";
        Configuration = GenerateConfiguration(env);

        if ((Settings?.SystemUnderTest?.ParallelRunning ?? false) == false)
            testThreadContainer.BaseContainer.Resolve<BrowserDriver>();
    }

    [AfterScenario]
    public static void AfterTestRunned()
        => BrowserDriver?.Dispose();
}