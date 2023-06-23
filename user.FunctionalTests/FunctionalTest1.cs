using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using NUnit.Framework;

namespace user.FunctionalTests
{
    public class Tests
    {
        private IWebDriver driver;

        [SetUp]
        public void Setup()
        {
            driver = new ChromeDriver();
        }

        [TearDown]
        public void TearDown()
        {
            driver.Quit();
        }

        [Test]
        public void FetchDataUsers()
        {
            //initiate
            driver.Url = "https://localhost:4000/user";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //accept unsafe ssl certificate
            IWebElement details = driver.FindElement(By.CssSelector("[id='details-button']"));
            details.Click();

            IWebElement detailsAccept = driver.FindElement(By.CssSelector("[id='proceed-link']"));
            detailsAccept.Click();

            //keycloak login
            IWebElement usernameInput = driver.FindElement(By.CssSelector("[id='username']"));
            usernameInput.SendKeys("admin");

            IWebElement passwordInput = driver.FindElement(By.CssSelector("[id='password']"));
            passwordInput.SendKeys("admin");

            IWebElement login = driver.FindElement(By.CssSelector("[id='kc-login']"));
            login.Click();

            //if needed for hamburger menu
            // IWebElement hamburger = driver.FindElement(By.CssSelector("[class*='mud-icon-root mud-svg-icon mud-icon-size-medium']"));
            // hamburger.Click();

            //close login popup
            IWebElement closeButton = driver.FindElement(By.XPath("//div[contains(@class,'mud-snackbar')]//button[contains(@class,'mud-button-root')]"));
            closeButton.Click();

            //navigate to user page
            IWebElement usersTab = driver.FindElement(By.XPath("//div[@class='mud-nav-link-text' and text()='Users']"));
            usersTab.Click();

            IWebElement link = driver.FindElement(By.CssSelector("[href='/user']"));
            link.Click();

            //check if table is being filled with data
            IWebElement table = driver.FindElement(By.CssSelector("[class='mud-table-root']"));
            List<IWebElement> rows = table.FindElements(By.CssSelector("tr")).ToList();
            bool isTableFilled = rows.Count > 0;

            if (!isTableFilled)
            {
                throw new Exception("The table is empty. Test failed.");
            }

            //screenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            // Set the file path and name for saving the screenshot
            string screenshotFilePath = "../../../TestResults/viewUserTable.png";

            // Save the screenshot to the specified file
            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);


        }

        [Test]
        public void AddUser_ValidData()
        {
            //initiate
            driver.Url = "https://localhost:4000/user/add";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //accept unsafe ssl certificate
            IWebElement details = driver.FindElement(By.CssSelector("[id='details-button']"));
            details.Click();

            IWebElement detailsAccept = driver.FindElement(By.CssSelector("[id='proceed-link']"));
            detailsAccept.Click();

            //keycloak login
            IWebElement usernameInput = driver.FindElement(By.CssSelector("[id='username']"));
            usernameInput.SendKeys("admin");

            IWebElement passwordInput = driver.FindElement(By.CssSelector("[id='password']"));
            passwordInput.SendKeys("admin");

            IWebElement login = driver.FindElement(By.CssSelector("[id='kc-login']"));
            login.Click();

            //if needed for hamburger menu
            // IWebElement hamburger = driver.FindElement(By.CssSelector("[class*='mud-icon-root mud-svg-icon mud-icon-size-medium']"));
            // hamburger.Click();

            //close login popup
            IWebElement closeButton = driver.FindElement(By.XPath("//div[contains(@class,'mud-snackbar')]//button[contains(@class,'mud-button-root')]"));
            closeButton.Click();

            //navigation
            IWebElement usersTab = driver.FindElement(By.XPath("//div[@class='mud-nav-link-text' and text()='Users']"));
            usersTab.Click();

            IWebElement link = driver.FindElement(By.CssSelector("[href='/user/add']"));
            link.Click();

            //filling in form
            IWebElement emailInput2 = driver.FindElement(By.XPath("//label[text()='Email']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            emailInput2.SendKeys("kip@gmail.com");

            IWebElement firstName = driver.FindElement(By.XPath("//label[text()='First name']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            firstName.SendKeys("kip");

            IWebElement lastName = driver.FindElement(By.XPath("//label[text()='Last name']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            lastName.SendKeys("stok");

            IWebElement phoneNumber = driver.FindElement(By.XPath("//label[text()='Phonenumber']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            phoneNumber.SendKeys("0623152322");

            IWebElement userName = driver.FindElement(By.XPath("//label[text()='Username']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            userName.SendKeys("Chicken");

            //sending form
            By addButtonLocator = By.XPath("//button[contains(@class, 'mud-button-root') and contains(@class, 'mud-button-filled-primary') and .//span[text()='Toevoegen']]");
            IWebElement addButton = driver.FindElement(addButtonLocator);
            addButton.Click();

            //check if user has been added
            bool isSnackbarDisplayed = driver.FindElements(By.XPath("//div[contains(@class, 'mud-snackbar') and contains(@class, 'mud-alert-filled-success')]")).Count > 0;
            if (!isSnackbarDisplayed)
            {
                // Snackbar exists
                throw new Exception("The snackbar does not exist or is not giving the right success. Test failed.");
            }

            //screenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string screenshotFilePath = "../../../TestResults/AddUser.png";

            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

        }

        [Test]
        public void AddUser_InvalidData()
        {
            //initiate
            driver.Url = "https://localhost:4000/user/add";
            driver.Manage().Timeouts().ImplicitWait = TimeSpan.FromSeconds(10);

            //accept unsafe ssl certificate
            IWebElement details = driver.FindElement(By.CssSelector("[id='details-button']"));
            details.Click();

            IWebElement detailsAccept = driver.FindElement(By.CssSelector("[id='proceed-link']"));
            detailsAccept.Click();

            //keycloak login
            IWebElement usernameInput = driver.FindElement(By.CssSelector("[id='username']"));
            usernameInput.SendKeys("admin");

            IWebElement passwordInput = driver.FindElement(By.CssSelector("[id='password']"));
            passwordInput.SendKeys("admin");

            IWebElement login = driver.FindElement(By.CssSelector("[id='kc-login']"));
            login.Click();

            //if needed for hamburger menu
            // IWebElement hamburger = driver.FindElement(By.CssSelector("[class*='mud-icon-root mud-svg-icon mud-icon-size-medium']"));
            // hamburger.Click();

            //close login popup
            IWebElement closeButton = driver.FindElement(By.XPath("//div[contains(@class,'mud-snackbar')]//button[contains(@class,'mud-button-root')]"));
            closeButton.Click();

            //navigation
            IWebElement usersTab = driver.FindElement(By.XPath("//div[@class='mud-nav-link-text' and text()='Users']"));
            usersTab.Click();

            IWebElement link = driver.FindElement(By.CssSelector("[href='/user/add']"));
            link.Click();

            //filling in form
            IWebElement emailInput2 = driver.FindElement(By.XPath("//label[text()='Email']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            emailInput2.SendKeys("kip@gmail.com");

            IWebElement firstName = driver.FindElement(By.XPath("//label[text()='First name']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            firstName.SendKeys("kip");

            IWebElement lastName = driver.FindElement(By.XPath("//label[text()='Last name']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            lastName.SendKeys("stok");

            IWebElement phoneNumber = driver.FindElement(By.XPath("//label[text()='Phonenumber']/ancestor::div[contains(@class,'mud-input-control')]/div/input"));
            phoneNumber.SendKeys("0623152322");

            //sending form
            By addButtonLocator = By.XPath("//button[contains(@class, 'mud-button-root') and contains(@class, 'mud-button-filled-primary') and .//span[text()='Toevoegen']]");
            IWebElement addButton = driver.FindElement(addButtonLocator);
            addButton.Click();

            //check if user was not added
            bool isSnackbarDisplayed = driver.FindElements(By.XPath("//div[contains(@class, 'mud-snackbar') and contains(@class, 'mud-alert-filled-error')]")).Count > 0;
            if (!isSnackbarDisplayed)
            {
                throw new Exception("The snackbar does not exist or is not giving the right error. Test failed.");
            }

            //screenshot
            ITakesScreenshot screenshotDriver = (ITakesScreenshot)driver;
            Screenshot screenshot = screenshotDriver.GetScreenshot();

            string screenshotFilePath = "../../../TestResults/AddUserInvalidData.png";

            screenshot.SaveAsFile(screenshotFilePath, ScreenshotImageFormat.Png);

        }
    }
}

// driver.Manage().Window.Size = new System.Drawing.Size(800, 600);
