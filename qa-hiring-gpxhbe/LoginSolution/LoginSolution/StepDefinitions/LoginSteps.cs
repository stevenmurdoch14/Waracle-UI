using LoginSolution.Pages;
using OnlineShopTests.Drivers;
using TechTalk.SpecFlow;
using System;
using OpenQA.Selenium;
using OpenQA.Selenium.Support.UI;


namespace LoginSolution.StepDefinitions
{
    [Binding]
    public class LoginSteps
    {
        private readonly WebDriverManager _webDriverManager;
        private LoginPage _loginPage;
        private InventoryPage _inventoryPage;

        public LoginSteps(WebDriverManager webDriverManager)
        {
            _webDriverManager = webDriverManager;
        }

        [BeforeScenario]
        public void Setup()
        {
            _webDriverManager.Start();
            _loginPage = new LoginPage(_webDriverManager.Driver);
            _inventoryPage = new InventoryPage(_webDriverManager.Driver);
        }

        [AfterScenario]
        public void TearDown()
        {
            _webDriverManager.Quit();
        }

        [Given(@"I navigate to the login page")]
        public void GivenINavigateToTheLoginPage()
        {
            _loginPage.Navigate();
        }

        [When(@"I login with username ""(.*)"" and password ""(.*)""")]
        public void WhenILoginWithUsernameAndPassword(string username, string password)
        {
            _loginPage.Login(username, password);
        }

        [Then(@"I should be redirected to the inventory page")]
        public void ThenIShouldBeRedirectedToTheInventoryPage()
        {
            if (!_inventoryPage.IsAt())
                throw new Exception("User was not redirected to the inventory page");
        }

        [Then(@"I should see an error message ""(.*)""")]
        public void ThenIShouldSeeAnErrorMessage(string expectedMessage)
        {
            var actualMessage = _loginPage.GetErrorMessage();
            if (!actualMessage.Contains(expectedMessage))
                throw new Exception($"Expected error message to contain '{expectedMessage}', but got '{actualMessage}'");
        }


        [Given(@"I login as ""(.*)""")]
        public void GivenILoginAs(string username)
        {
            _loginPage.Navigate();
            _loginPage.Login(username, "secret_sauce");
        }

        [Then(@"I should see the appropriate image displayed against Sauce Labs BackPack")]
        public void ThenIShouldSeeTheBrokenImagePlaceholderOnTheInventoryPage()
        {
            if (!_inventoryPage.IsBrokenImageDisplayed())
                throw new Exception("Broken image placeholder is not displayed for problem user.");
        }

        [When(@"the inventory page loads successfully")]
        public void ThenTheInventoryPageShouldLoadSuccessfullyDespiteDelays()
        {
            if (!_inventoryPage.IsAt())
                throw new Exception("Inventory page did not load for performance glitch user");
        }



        [Then("I can navigate to the backpack page without performance delays over (.*) seconds")]
        public void ThenICanNavigateToTheBackpackPageWithoutPerformanceDelaysOverSeconds(int maxSeconds)
        {
            var stopwatch = System.Diagnostics.Stopwatch.StartNew();

            // Click the Sauce Labs Backpack item
            _inventoryPage.ClickBackpackItem();

            // Wait for navigation to complete
            var wait = new WebDriverWait(_webDriverManager.Driver, TimeSpan.FromSeconds(maxSeconds));
            wait.Until(driver => driver.Url.Contains("inventory-item.html?id=4"));

            stopwatch.Stop();

            if (stopwatch.Elapsed.TotalSeconds > maxSeconds)
            {
                throw new Exception($"Navigation took {stopwatch.Elapsed.TotalSeconds:F2} seconds, exceeding the allowed {maxSeconds} seconds.");
            }

            // Optional: Validate final URL
            var currentUrl = _webDriverManager.Driver.Url;
            if (!currentUrl.EndsWith("inventory-item.html?id=4"))
            {
                throw new Exception($"Unexpected URL after navigation: {currentUrl}");
            }
        }

}

    }