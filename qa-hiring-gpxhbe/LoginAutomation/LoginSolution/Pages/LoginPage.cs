using OpenQA.Selenium;

namespace LoginSolution.Pages
{
    public class LoginPage
    {
        private readonly IWebDriver _driver;
        public LoginPage(IWebDriver driver) => _driver = driver;

        private IWebElement Username => _driver.FindElement(By.Id("user-name"));
        private IWebElement Password => _driver.FindElement(By.Id("password"));
        private IWebElement LoginButton => _driver.FindElement(By.Id("login-button"));
        private IWebElement ErrorMessage => _driver.FindElement(By.CssSelector("h3[data-test='error']"));

        public void Navigate() => _driver.Navigate().GoToUrl("https://qa-challenge.codesubmit.io");

        public void Login(string username, string password)
        {
            Username.SendKeys(username);
            Password.SendKeys(password);
            LoginButton.Click();
        }

        public string GetErrorMessage() => ErrorMessage.Text;
    }
}