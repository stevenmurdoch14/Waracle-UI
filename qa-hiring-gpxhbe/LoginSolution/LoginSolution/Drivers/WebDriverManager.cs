using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;

namespace OnlineShopTests.Drivers
{
    public class WebDriverManager
    {
        public IWebDriver Driver { get; private set; }

        public void Start()
        {
            var options = new ChromeOptions();
            options.AddArgument("--start-maximized");
            Driver = new ChromeDriver(options);
        }

        public void Quit()
        {
            Driver.Quit();
        }
    }
}