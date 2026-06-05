using AventStack.ExtentReports;

namespace Framework.Reporting;

// Scenario-scoped reporting context (stored in SpecFlow container)
public class ReportContext
{
    public ExtentReports Extent { get; set; }
    public ExtentTest Scenario { get; set; }
    public ExtentTest Step { get; set; }
}