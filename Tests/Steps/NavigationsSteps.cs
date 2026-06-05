using Framework.Base;
using OpenQA.Selenium;
using Reqnroll;

namespace UITests.Steps;

[Binding]
public class NavigationsSteps : BasePage
{
    private readonly NavigationsPage _nPage;

    public NavigationsSteps(IWebDriver driver) : base(driver)
    {
        _nPage = new NavigationsPage(driver);
    }

    [Given("Click on Checkbox")]
    public void GivenClickOnCheckbox()
    {
        _nPage.ClickOnCheckbox();
    }
}