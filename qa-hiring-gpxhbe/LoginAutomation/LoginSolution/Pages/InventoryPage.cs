using OpenQA.Selenium;
using System.Linq;

namespace LoginSolution.Pages
{
    public class InventoryPage
    {
        private readonly IWebDriver _driver;
        public InventoryPage(IWebDriver driver) => _driver = driver;

        public bool IsAt() => _driver.Url.Contains("inventory");

        public bool AreImagesBroken()
        {
            var images = _driver.FindElements(By.CssSelector(".inventory_item_img img"));
            return images.Any(img => (bool)((IJavaScriptExecutor)_driver).ExecuteScript(
                "return arguments[0].naturalWidth == 0", img));
        }
        
        public void ClickProductByName(string productName)
        {
            var productElement = _driver.FindElement(By.XPath($"//div[@data-test='inventory-item-name' and text()='{productName}']"));
            productElement.Click();
        }

        public void ClickBackpackItem()
        {
            var backpackItem = _driver.FindElement(By.XPath("//div[text()='Sauce Labs Backpack']/ancestor::a"));
            backpackItem.Click();
        }

        public bool IsBrokenImageDisplayed()
        {
            try
            {
                var brokenImage = _driver.FindElement(By.CssSelector("img[src='/static/media/sl-404.168b1cce.jpg']"));
                return brokenImage.Displayed;
            }
            catch (NoSuchElementException)
            {
                return false;
            }
        }

    }
}