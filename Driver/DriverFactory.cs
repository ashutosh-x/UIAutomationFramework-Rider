using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Firefox;

namespace Framework.Driver;

public static class DriverFactory
{
    public static IWebDriver CreateDriver(BrowserType browserType)
    {
        return browserType switch
        {
            BrowserType.Chrome => new ChromeDriver(GetChromeOptions()),
            BrowserType.Edge => new EdgeDriver(),
            BrowserType.Firefox => new FirefoxDriver(),
            _ => throw new ArgumentException($"Unsupported browser type: {browserType}")
        };
    }

    private static ChromeOptions GetChromeOptions()
    {
        var options = new ChromeOptions();
        options.AddArgument("--start-maximized");
        options.AddArgument("--disable-notifications");
        options.AddArgument("--disable-popup-blocking");
        return options;
    }
}