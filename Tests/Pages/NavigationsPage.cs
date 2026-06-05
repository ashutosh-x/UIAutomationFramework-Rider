using Framework.Base;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;
using Reqnroll;
using SeleniumExtras.WaitHelpers;

namespace UITests;

[Binding]
public class NavigationsPage : BasePage
{
    public NavigationsPage(IWebDriver driver) : base(driver)
    {
        
    }

    By elemCheckboxLocator = By.XPath("//input[@type = 'checkbox']");
    By elemRemoveBtn = By.XPath("//button[@type = 'button' and contains(text(), 'Remove')]");
    By msgTextChkBox = By.XPath("//*[@id = 'checkbox-example']/p[@id='message']");
    
    //Locators for Enable Btn
    By elemEnableBtn = By.XPath("//button[@type = 'button' and contains(text(), 'Enable')]");

    private void ExplicitWait(By locator)
    {
        WebDriverWait wait = new WebDriverWait(Driver, TimeSpan.FromSeconds(60));
        wait.Until(ExpectedConditions.ElementToBeClickable(locator));
    }
    
    public void ClickOnCheckbox()
    {
        ExplicitWait(elemCheckboxLocator);
        Driver.FindElement(elemCheckboxLocator).Click();
        Driver.FindElement(elemRemoveBtn).Click();
        AssertMessageForChkBox();
        
        Thread.Sleep(10000);
    }

    private bool IsCheckboxRemoved()
    {
        return Driver.FindElement(elemCheckboxLocator).Enabled;
    }

    public void ClickOnEnableBtn()
    {
        ExplicitWait(elemEnableBtn);
        Driver.FindElement(elemEnableBtn).Click();
    }

    private void AssertMessageForChkBox()
    {
        ExplicitWait(msgTextChkBox);
        try
        {
            Assert.That(Driver.FindElement(msgTextChkBox).Text, 
                Is.EqualTo("It's gone!"));
        }
        catch (Exception e)
        {
            Console.WriteLine(e.StackTrace);
            throw;
        }
        
    }
}