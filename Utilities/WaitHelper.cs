using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.WaitHelpers;

namespace Framework.Utilities;

public class WaitHelper
{
    private readonly WebDriverWait _wait;

    public WaitHelper(IWebDriver driver, int timeoutSeconds)
    {
        _wait = new WebDriverWait(driver, TimeSpan.FromSeconds(timeoutSeconds));
    }

    public IWebElement WaitForVisible(By locator)
    {
        return _wait.Until(ExpectedConditions.ElementIsVisible(locator));
    }

    public IWebElement WaitForClickable(By locator)
    {
        return _wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }
}