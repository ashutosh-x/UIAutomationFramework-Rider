using AventStack.ExtentReports;
using AventStack.ExtentReports.Reporter;

namespace Framework.Reporting;

public static class ExtentManager
{
    private static ExtentReports _extent;

    public static ExtentReports GetExtent(string reportFolderPath)
    {
        if (_extent != null)
            return _extent;

        Directory.CreateDirectory(reportFolderPath);

        var reportFilePath = Path.Combine(reportFolderPath, "AutomationReport.html");

        var reporter = new ExtentSparkReporter(reportFilePath);

        _extent = new ExtentReports();
        _extent.AttachReporter(reporter);

        return _extent;
    }

    public static void Flush()
    {
        _extent?.Flush();
    }
}