using OpenQA.Selenium;

namespace Framework.Utilities;

public static class ScreenshotHelper
{
    public static string TakeScreenshot(IWebDriver driver, string folderPath, string scenarioName)
    {
        Directory.CreateDirectory(folderPath);

        var safeScenarioName = string.Join("_", scenarioName.Split(Path.GetInvalidFileNameChars()));
        var fileName = $"{safeScenarioName}_{DateTime.Now:yyyyMMdd_HHmmss}.png";

        var filePath = Path.Combine(folderPath, fileName);

        var screenshot = ((ITakesScreenshot)driver).GetScreenshot();
        screenshot.SaveAsFile(filePath);

        return filePath;
    }
}