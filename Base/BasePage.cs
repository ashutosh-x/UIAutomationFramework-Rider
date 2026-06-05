using Framework.Config;
using Framework.Utilities;
using OpenQA.Selenium;

namespace Framework.Base;

public abstract class BasePage
{
    protected BasePage(IWebDriver driver)
    {
        Driver = driver;
        var settings = ConfigReader.Settings;
        Wait = new WaitHelper(driver, settings.ExplicitWaitSeconds);
    }

    protected IWebDriver Driver { get; }
    private WaitHelper Wait { get; }

    protected void Click(By locator)
    {
        Wait.WaitForClickable(locator).Click();
    }

    protected void Type(By locator, string text)
    {
        var element = Wait.WaitForVisible(locator);
        element.Clear();
        element.SendKeys(text);
    }

    protected bool IsDisplayed(By locator)
    {
        try
        {
            return Wait.WaitForVisible(locator).Displayed;
        }
        catch
        {
            return false;
        }
    }
}