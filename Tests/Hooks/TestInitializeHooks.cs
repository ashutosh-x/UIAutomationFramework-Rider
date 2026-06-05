using Framework.Config;
using Framework.Driver;
using Framework.Reporting;
using Framework.Utilities;
using OpenQA.Selenium;
using Reqnroll;
using Reqnroll.BoDi;
using Serilog;

namespace UITests.Hooks;

[Binding]
public class TestInitializeHooks
{
    private readonly IObjectContainer _container;
    private readonly ScenarioContext _scenarioContext;

    private IWebDriver _driver;
    private ReportContext _reportContext;

    public TestInitializeHooks(ScenarioContext scenarioContext, IObjectContainer container)
    {
        _scenarioContext = scenarioContext;
        _container = container;
    }

    [BeforeTestRun]
    public static void BeforeTestRun()
    {
        var settings = ConfigReader.Settings;

        Directory.CreateDirectory(settings.LogsPath);

        Log.Logger = new LoggerConfiguration()
            .WriteTo.Console()
            .WriteTo.File(Path.Combine(settings.LogsPath, "log-.txt"), rollingInterval: RollingInterval.Day)
            .CreateLogger();

        ExtentManager.GetExtent(settings.ReportPath);
    }

    [BeforeScenario]
    public void BeforeScenario()
    {
        var settings = ConfigReader.Settings;

        var browserType = Enum.Parse<BrowserType>(settings.Browser, true);

        _driver = DriverFactory.CreateDriver(browserType);
        _driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(settings.ImplicitWaitSeconds);
        _driver.Navigate().GoToUrl(settings.BaseUrl);

        // Register WebDriver into SpecFlow DI container (Scenario scoped)
        _container.RegisterInstanceAs(_driver);

        // Setup report context (Scenario scoped)
        _reportContext = new ReportContext
        {
            Extent = ExtentManager.GetExtent(settings.ReportPath),
            Scenario = ExtentManager.GetExtent(settings.ReportPath).CreateTest(_scenarioContext.ScenarioInfo.Title)
        };

        _container.RegisterInstanceAs(_reportContext);

        Log.Information($"Scenario started: {_scenarioContext.ScenarioInfo.Title}");
    }

    [BeforeStep]
    public void BeforeStep()
    {
        if (_reportContext == null)
            return;

        var stepText = _scenarioContext.StepContext.StepInfo.Text;
        var stepType = _scenarioContext.StepContext.StepInfo.StepDefinitionType.ToString();

        _reportContext.Step = _reportContext.Scenario.CreateNode($"{stepType}: {stepText}");
    }

    [AfterStep]
    public void AfterStep()
    {
        if (_reportContext == null)
            return;

        if (_scenarioContext.TestError != null)
        {
            _reportContext.Step.Fail(_scenarioContext.TestError.Message);
            Log.Error(_scenarioContext.TestError, "Step failed");
        }
        else
        {
            _reportContext.Step.Pass("Step passed");
        }
    }

    [AfterScenario]
    public void AfterScenario()
    {
        var settings = ConfigReader.Settings;

        if (_scenarioContext.TestError != null && _driver != null && _reportContext.Scenario != null)
        {
            var screenshotPath = ScreenshotHelper.TakeScreenshot(
                _driver,
                settings.ScreenshotPath,
                _scenarioContext.ScenarioInfo.Title
            );

            _reportContext.Scenario.Fail(_scenarioContext.TestError);
            _reportContext.Scenario.AddScreenCaptureFromPath(screenshotPath);

            Log.Error(_scenarioContext.TestError, "Scenario failed");
        }
        else
        {
            if (_reportContext.Scenario != null) _reportContext.Scenario.Pass("Scenario passed");
        }

        try
        {
            if (_driver != null)
            {
                _driver.Quit();
                _driver.Dispose();
            }
        }
        catch
        {
            // ignored
        }
    }

    [AfterTestRun]
    public static void AfterTestRun()
    {
        ExtentManager.Flush();
    }
}