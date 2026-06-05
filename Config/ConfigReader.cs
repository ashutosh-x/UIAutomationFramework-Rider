using Microsoft.Extensions.Configuration;

namespace Framework.Config;

public static class ConfigReader
{
    private static FrameworkSettings _settings;

    public static FrameworkSettings Settings
    {
        get
        {
            if (_settings != null)
                return _settings;

            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json", false, true)
                .Build();

            _settings = config.GetSection("FrameworkSettings").Get<FrameworkSettings>();

            if (_settings == null)
                throw new Exception("FrameworkSettings section missing in appsettings.json");

            return _settings;
        }
    }
}