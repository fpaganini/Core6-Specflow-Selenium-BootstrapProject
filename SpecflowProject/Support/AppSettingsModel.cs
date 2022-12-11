using Microsoft.Extensions.Configuration;

namespace SpecflowProject.Support;

public class AppSettingsModel
{
    public SystemUnderTestModel? SystemUnderTest { get; set; }

    public static AppSettingsModel Bind(IConfiguration config)
    {
        var appSettings = new AppSettingsModel();
        config.Bind(appSettings);
        return appSettings;
    }

}

public class SystemUnderTestModel
{
    public string? BaseURL { get; set; }
    public bool? ParallelRunning { get; set; }
    public bool? PrintScreenEachStep { get; set; }
    public bool? PrintScreenOnError { get; set; }
}
