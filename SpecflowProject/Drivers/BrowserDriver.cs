using Microsoft.Extensions.Configuration;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;
using OpenQA.Selenium.IE;
using SpecflowProject.Hooks;

namespace SpecflowProject.Drivers;

public class BrowserDriver : IDisposable
{
    private readonly Lazy<IWebDriver> _lazyWebDriver;
    private bool _isDisposed;

    public BrowserDriver()
        => _lazyWebDriver = new Lazy<IWebDriver>(CreateWebDriver);

    public IWebDriver WebDriver => _lazyWebDriver.Value;

    private IWebDriver CreateWebDriver()
    {
        var driverSection = ConfigHook.Configuration?.GetSection("Driver");
        var driverService = driverSection?["DriverService"];

        if (driverService.IsNullOrWhiteSpaceOrEquals("CHROME", StringComparison.InvariantCultureIgnoreCase))
            return CreateNewChromeDriver(driverSection);
        else if (driverService!.Equals("FIREFOX", StringComparison.InvariantCultureIgnoreCase))
            return CreateNewFirefoxDriver(driverSection);
        else if (driverService!.Equals("EDGE", StringComparison.InvariantCultureIgnoreCase))
            return CreateNewEdgeDriver(driverSection);
        else if (driverService!.Equals("INTERNET_EXPLORER", StringComparison.InvariantCultureIgnoreCase))
            return CreateNewInternetExplorerDriver(driverSection);
        else
            throw new NotImplementedException($"Driver {driverService} não implementado. Os drivers suportados são [CHROME, FIREFOX, EDGE, INTERNET_EXPLORER]");
    }


    private IWebDriver CreateNewChromeDriver(IConfigurationSection? config)
    {
        var customDriverPath = config?["CustomDriverPath"];

        ChromeDriverService driverService;
        if (customDriverPath.IsNullOrWhiteSpace())
            driverService = ChromeDriverService.CreateDefaultService();
        else
            driverService = ChromeDriverService.CreateDefaultService(customDriverPath);

        var driverOptions = new ChromeOptions();

        var headless = config.GetValue<bool?>("Headless");
        var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
        var noSandbox = config.GetValue<bool?>("NoSandbox");
        var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
        var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

        if (noSandbox.HasValue && noSandbox == true)
            driverOptions.AddArgument("--no-sandbox");

        if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
            driverOptions.AddArgument("--disable-dev-shm-usage");

        if (headless.HasValue && headless == true)
            driverOptions.AddArgument("--headless");

        if (browserLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

        if (driverLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

        return new ChromeDriver(driverService, driverOptions);
    }

    private IWebDriver CreateNewFirefoxDriver(IConfigurationSection? config)
    {
        var customDriverPath = config?["CustomDriverPath"];

        FirefoxDriverService driverService;
        if (customDriverPath.IsNullOrWhiteSpace())
            driverService = FirefoxDriverService.CreateDefaultService();
        else
            driverService = FirefoxDriverService.CreateDefaultService(customDriverPath);

        var driverOptions = new FirefoxOptions();

        var headless = config.GetValue<bool?>("Headless");
        var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
        var noSandbox = config.GetValue<bool?>("NoSandbox");
        var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
        var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

        if (noSandbox.HasValue && noSandbox == true)
            driverOptions.AddArgument("--no-sandbox");

        if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
            driverOptions.AddArgument("--disable-dev-shm-usage");

        if (headless.HasValue && headless == true)
            driverOptions.AddArgument("--headless");

        if (browserLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

        if (driverLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

        return new FirefoxDriver(driverService, driverOptions);
    }

    private IWebDriver CreateNewEdgeDriver(IConfigurationSection? config)
    {
        var customDriverPath = config?["CustomDriverPath"];

        customDriverPath.Should().NotBeNullOrWhiteSpace("Não há um driver embutido para Edge, informe o caminho do drive em Driver->CustomDriverPath");

        EdgeDriverService driverService;
        if (customDriverPath.IsNullOrWhiteSpace())
            driverService = EdgeDriverService.CreateDefaultService();
        else
            driverService = EdgeDriverService.CreateDefaultService(customDriverPath);

        var driverOptions = new EdgeOptions();

        var headless = config.GetValue<bool?>("Headless");
        var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
        var noSandbox = config.GetValue<bool?>("NoSandbox");
        var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
        var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

        if (noSandbox.HasValue && noSandbox == true)
            driverOptions.AddArgument("--no-sandbox");

        if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
            driverOptions.AddArgument("--disable-dev-shm-usage");

        if (headless.HasValue && headless == true)
            driverOptions.AddArgument("--headless");

        if (browserLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

        if (driverLogLevel.HasValue)
            driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

        return new EdgeDriver(driverService, driverOptions);
    }


    private IWebDriver CreateNewInternetExplorerDriver(IConfigurationSection? config)
    {
        var customDriverPath = config?["CustomDriverPath"];

        customDriverPath.Should().NotBeNullOrWhiteSpace("Não há um driver embutido para Internet Explorer, informe o caminho do drive em Driver->CustomDriverPath");

        InternetExplorerDriverService driverService;
        if (customDriverPath.IsNullOrWhiteSpace())
            driverService = InternetExplorerDriverService.CreateDefaultService();
        else
            driverService = InternetExplorerDriverService.CreateDefaultService(customDriverPath);

        var driverOptions = new InternetExplorerOptions();

        return new InternetExplorerDriver(driverService, driverOptions);
    }

    public void Dispose()
    {
        if (_isDisposed)
            return;

        if (_lazyWebDriver.IsValueCreated)
        {
            WebDriver.Quit();
            WebDriver.Dispose();
        }

        _isDisposed = true;
    }
}
//using Microsoft.Extensions.Configuration;
//using OpenQA.Selenium;
//using OpenQA.Selenium.Chrome;
//using OpenQA.Selenium.Edge;
//using OpenQA.Selenium.Firefox;
//using OpenQA.Selenium.IE;
//using TechTalk.SpecFlow.Infrastructure;

//namespace SpecflowProject.Drivers
//{
//    public class BrowserDriver : IDisposable
//    {
//        private readonly ISpecFlowOutputHelper _specFlowOutputHelper;
//        private readonly Lazy<IWebDriver> _lazyWebDriver;
//        private bool _isDisposed;

//        public BrowserDriver(ISpecFlowOutputHelper specFlowOutputHelper)
//        {
//            _specFlowOutputHelper = specFlowOutputHelper;
//            _lazyWebDriver = new Lazy<IWebDriver>(CreateNewWebDriver());
//        }

//        public IWebDriver WebDriver => _lazyWebDriver.Value;

//        private IWebDriver CreateNewWebDriver()
//        {
//            //var driverSection = StartupHook.Configuration.GetSection("Driver");
//            //var driverService = driverSection?["DriverService"];

//            //if (driverService.IsNullOrWhiteSpaceOrEquals("CHROME", StringComparison.InvariantCultureIgnoreCase))
//            //return CreateNewChromeDriver(driverSection);
//            //else if (driverService!.Equals("FIREFOX", StringComparison.InvariantCultureIgnoreCase))
//            //    return CreateNewFirefoxDriver(driverSection);
//            //else if (driverService!.Equals("EDGE", StringComparison.InvariantCultureIgnoreCase))
//            //    return CreateNewEdgeDriver(driverSection);
//            //else if (driverService!.Equals("INTERNET_EXPLORER", StringComparison.InvariantCultureIgnoreCase))
//            //    return CreateNewInternetExplorerDriver(driverSection);
//            //else
//            //    throw new NotImplementedException($"Driver {driverService} não implementado. Os drivers suportados são [CHROME, FIREFOX, EDGE, INTERNET_EXPLORER]");


//            ChromeDriverService driverService;

//            driverService = ChromeDriverService.CreateDefaultService();

//            var driverOptions = new ChromeOptions();


//            var driver = new ChromeDriver(driverService, driverOptions);

//            _specFlowOutputHelper.WriteLine("Browser launched");

//            return driver;
//        }

//        private IWebDriver CreateNewChromeDriver(IConfigurationSection? config)
//        {
//            var customDriverPath = config?["CustomDriverPath"];

//            ChromeDriverService driverService;
//            if (customDriverPath.IsNullOrWhiteSpace())
//                driverService = ChromeDriverService.CreateDefaultService();
//            else
//                driverService = ChromeDriverService.CreateDefaultService(customDriverPath);

//            var driverOptions = new ChromeOptions();

//            var headless = config.GetValue<bool?>("Headless");
//            var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
//            var noSandbox = config.GetValue<bool?>("NoSandbox");
//            var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
//            var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

//            if (noSandbox.HasValue && noSandbox == true)
//                driverOptions.AddArgument("--no-sandbox");

//            if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
//                driverOptions.AddArgument("--disable-dev-shm-usage");

//            if (headless.HasValue && headless == true)
//                driverOptions.AddArgument("--headless");

//            if (browserLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

//            if (driverLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

//            return new ChromeDriver(driverService, driverOptions);
//        }

//        private IWebDriver CreateNewFirefoxDriver(IConfigurationSection? config)
//        {
//            var customDriverPath = config?["CustomDriverPath"];

//            FirefoxDriverService driverService;
//            if (customDriverPath.IsNullOrWhiteSpace())
//                driverService = FirefoxDriverService.CreateDefaultService();
//            else
//                driverService = FirefoxDriverService.CreateDefaultService(customDriverPath);

//            var driverOptions = new FirefoxOptions();

//            var headless = config.GetValue<bool?>("Headless");
//            var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
//            var noSandbox = config.GetValue<bool?>("NoSandbox");
//            var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
//            var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

//            if (noSandbox.HasValue && noSandbox == true)
//                driverOptions.AddArgument("--no-sandbox");

//            if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
//                driverOptions.AddArgument("--disable-dev-shm-usage");

//            if (headless.HasValue && headless == true)
//                driverOptions.AddArgument("--headless");

//            if (browserLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

//            if (driverLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

//            return new FirefoxDriver(driverService, driverOptions);
//        }

//        private IWebDriver CreateNewEdgeDriver(IConfigurationSection? config)
//        {
//            var customDriverPath = config?["CustomDriverPath"];

//            customDriverPath.Should().NotBeNullOrWhiteSpace("Não há um driver embutido para Edge, informe o caminho do drive em Driver->CustomDriverPath");

//            EdgeDriverService driverService;
//            if (customDriverPath.IsNullOrWhiteSpace())
//                driverService = EdgeDriverService.CreateDefaultService();
//            else
//                driverService = EdgeDriverService.CreateDefaultService(customDriverPath);

//            var driverOptions = new EdgeOptions();

//            var headless = config.GetValue<bool?>("Headless");
//            var disableDevShmUsage = config.GetValue<bool?>("DisableDevShmUsage");
//            var noSandbox = config.GetValue<bool?>("NoSandbox");
//            var browserLogLevel = config.GetValue<LogLevel?>("BrowserLogLevel");
//            var driverLogLevel = config.GetValue<LogLevel?>("DriverLogLevel");

//            if (noSandbox.HasValue && noSandbox == true)
//                driverOptions.AddArgument("--no-sandbox");

//            if (disableDevShmUsage.HasValue && disableDevShmUsage == true)
//                driverOptions.AddArgument("--disable-dev-shm-usage");

//            if (headless.HasValue && headless == true)
//                driverOptions.AddArgument("--headless");

//            if (browserLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Browser, browserLogLevel.GetValueOrDefault());

//            if (driverLogLevel.HasValue)
//                driverOptions.SetLoggingPreference(LogType.Driver, driverLogLevel.GetValueOrDefault());

//            return new EdgeDriver(driverService, driverOptions);
//        }


//        private IWebDriver CreateNewInternetExplorerDriver(IConfigurationSection? config)
//        {
//            var customDriverPath = config?["CustomDriverPath"];

//            customDriverPath.Should().NotBeNullOrWhiteSpace("Não há um driver embutido para Internet Explorer, informe o caminho do drive em Driver->CustomDriverPath");

//            InternetExplorerDriverService driverService;
//            if (customDriverPath.IsNullOrWhiteSpace())
//                driverService = InternetExplorerDriverService.CreateDefaultService();
//            else
//                driverService = InternetExplorerDriverService.CreateDefaultService(customDriverPath);

//            var driverOptions = new InternetExplorerOptions();

//            return new InternetExplorerDriver(driverService, driverOptions);
//        }


//        public void Dispose()
//        {
//            if (_isDisposed)
//                return;

//            if (_lazyWebDriver.IsValueCreated)
//                WebDriver.Quit();

//            _isDisposed = true;
//        }
//    }
//}
