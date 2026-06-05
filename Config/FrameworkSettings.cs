namespace Framework.Config;

public class FrameworkSettings
{
    public string Browser { get; set; } = "Chrome";
    public string BaseUrl { get; set; } = "";

    public int ImplicitWaitSeconds { get; set; } = 0;
    public int ExplicitWaitSeconds { get; set; } = 15;

    public string ReportPath { get; set; } = "TestResults/Reports";
    public string ScreenshotPath { get; set; } = "TestResults/Screenshots";
    public string LogsPath { get; set; } = "TestResults/Logs";
}