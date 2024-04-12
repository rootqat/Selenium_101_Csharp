using Microsoft.VisualStudio.TestPlatform.CommunicationUtilities;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
/* For using Remote Selenium WebDriver */
using OpenQA.Selenium.Remote;
using OpenQA.Selenium.Support.UI;


[assembly: Parallelize(Workers = 6, Scope = ExecutionScope.MethodLevel)]

namespace ParallelLTSelenium
{
    [TestClass]
    public class ParallelLTTests
    {

        IWebDriver driver;

        /* Profile - https://accounts.lambdatest.com/detail/profile */
        string username = "rootqat"; 
        string accesskey = "Iqjmon32WEuPHsYfQHveACFfDx1QFS5vWdP2bl8a43YvMPgMQo";
        string gridURL = "@hub.lambdatest.com/wd/hub";

        DesiredCapabilities capabilities;
       
        [TestInitialize]
        public void setupInit()
        {
            capabilities = new DesiredCapabilities();
            capabilities.SetCapability("user", username);
            capabilities.SetCapability("accessKey", accesskey);
        }

        [DataTestMethod]
        [DataRow("chrome", "latest", "Windows 10","Scenario01")]
        [DataRow("firefox", "latest", "Windows 10","Scenario01")]
       

        [TestMethod]
        public void TestScenario01(string browser, string version, string os,string scenario)
        {

            capabilities.SetCapability("browserName", browser);
            capabilities.SetCapability("version", version);
            capabilities.SetCapability("platform", os);
            capabilities.SetCapability("build", "Parallel Testing -Selnium101");
            capabilities.SetCapability("name", scenario);

            driver = new RemoteWebDriver(new Uri("https://" + username + ":" + accesskey + gridURL), capabilities, TimeSpan.FromSeconds(2000));

            driver.Url = "https://www.lambdatest.com/selenium-playground/";
            driver.Manage().Window.Maximize();
            IWebElement SimpleFormDemoLink = driver.FindElement(By.XPath("//a[contains(.,'Simple Form Demo')]"));
            SimpleFormDemoLink.Click();

            string Expectedurl = driver.Url;
            string Actualurl = "simple-form-demo";
            if (Expectedurl.Contains(Actualurl))
            {
                Console.WriteLine("URL matched");
            }
            else
            {
                Console.WriteLine("URL does not matched!");
            }

            string message = "Welcome to LambdaTest.";
            IWebElement textMessageBox = driver.FindElement(By.XPath("//div/input[@class='border border-gray-550 w-full h-35 rounded px-10']"));
            Thread.Sleep(1000);
            textMessageBox.SendKeys(message);

            Thread.Sleep(1000);
            IWebElement button = driver.FindElement(By.XPath("//button[@id='showInput']"));
            button.Click();

            IWebElement lblMesasage = driver.FindElement(By.XPath("//P[@id='message']"));
            string expectedMessage = lblMesasage.Text;

            Assert.AreEqual(message, expectedMessage);
        }

        [DataTestMethod]
        [DataRow("chrome", "latest", "Windows 10", "Scenario02")]
        [DataRow("firefox", "latest", "Windows 10", "Scenario02")]


        [TestMethod]
        public void TestScenario02(string browser, string version, string os,string scenario)
        {

            capabilities.SetCapability("browserName", browser);
            capabilities.SetCapability("version", version);
            capabilities.SetCapability("platform", os);
            capabilities.SetCapability("build", "Parallel Testing -Selnium101");
            capabilities.SetCapability("name", scenario);

            driver = new RemoteWebDriver(new Uri("https://" + username + ":" + accesskey + gridURL), capabilities, TimeSpan.FromSeconds(2000));

            driver.Url = "https://www.lambdatest.com/selenium-playground/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);

            IWebElement DrageAndDroplink = driver.FindElement(By.XPath("//a[contains(.,'Drag & Drop Sliders')]"));
            DrageAndDroplink.Click();

            Thread.Sleep(1000);
            IWebElement slider3 = driver.FindElement(By.XPath("//*[@id='slider3']/div/input"));
            // js.executeScript("arguments[0].scrollIntoView(true);", slider3);
            Thread.Sleep(1000);
            Actions move = new Actions(driver);
            Actions action = (Actions)move.DragAndDropToOffset(slider3, 216, 0);
            action.Perform();

            IWebElement Expected_Range = driver.FindElement(By.XPath("//*[@id='slider3']/div/output"));
            string expectedValueAfterDrag = "95";
            string ActualRangeAfterDrag = Expected_Range.Text;

            Assert.AreEqual(expectedValueAfterDrag, ActualRangeAfterDrag);
            if (ActualRangeAfterDrag.Equals(expectedValueAfterDrag))
            {
                Console.WriteLine("Range is matched");
            }
            else
            {
                Console.WriteLine("Range is not matched!");
            }
        }


        [DataTestMethod]
        [DataRow("chrome", "latest", "Windows 10", "Scenario03")]
        [DataRow("firefox", "latest", "Windows 10", "Scenario03")]


        [TestMethod]
        public void TestScenario03(string browser, string version, string os,string scenario)
        {
            capabilities.SetCapability("browserName", browser);
            capabilities.SetCapability("version", version);
            capabilities.SetCapability("platform", os);
            capabilities.SetCapability("build", "Parallel Testing -Selnium101");
            capabilities.SetCapability("name", scenario);
            driver = new RemoteWebDriver(new Uri("https://" + username + ":" + accesskey + gridURL), capabilities, TimeSpan.FromSeconds(2000));

            driver.Url = "https://www.lambdatest.com/selenium-playground/";
            driver.Manage().Window.Maximize();
            Thread.Sleep(2000);


            IWebElement InputFormLink = driver.FindElement(By.XPath("//a[@href='https://www.lambdatest.com/selenium-playground/input-form-demo']"));
            InputFormLink.Click();

            IWebElement submit = driver.FindElement(By.XPath("//div[@class='text-right mt-20']/button"));
            submit.Click();

            Thread.Sleep(1000);

            IWebElement name = driver.FindElement(By.XPath("//div[@class='form-group w-4/12 smtablet:w-full text-section pr-20 smtablet:pr-0']/input[@type='text']"));
            string ActualMessageValidation = name.GetAttribute("validationMessage");
            string expectedMessageValidation = "Please fill out this field.";
            Assert.AreEqual(ActualMessageValidation, expectedMessageValidation);

            if (ActualMessageValidation.Equals(expectedMessageValidation))
            {
                Console.WriteLine("Validation is properly appear.");
            }
            else
            {
                Console.WriteLine("Validation is not properly appear.");
            }

            name.SendKeys("TestName");

            IWebElement email = driver.FindElement(By.XPath("//div[@class='form-group w-4/12 smtablet:w-full text-section pr-20 smtablet:pr-0']/input[@type='email']"));
            email.SendKeys("Test123@gmail.com");

            IWebElement password = driver.FindElement(By.XPath("//div[@class='form-group w-4/12 smtablet:w-full']/input[@type='password']"));
            password.SendKeys("Test@1234");

            IWebElement company = driver.FindElement(By.XPath("//*[@id='company']"));
            company.SendKeys("TestCompany");

            IWebElement website = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full']/input[@id=\"websitename\"]"));
            website.SendKeys("Testdomain.com");

            IWebElement country = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full pr-20 smtablet:pr-0']/select[@name='country']"));
            SelectElement select = new SelectElement(country);
            select.SelectByText("United States");

            IWebElement city = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full']/input[@id='inputCity']"));
            city.SendKeys("TestCity");

            IWebElement address1 = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full pr-20 smtablet:pr-0']/input[@id='inputAddress1']"));
            address1.SendKeys("TestAddress1");

            IWebElement address2 = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full']/input[@id='inputAddress2']"));
            address2.SendKeys("TestAddress2");

            IWebElement state = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full pr-20 smtablet:pr-0']/input[@id='inputState']"));
            state.SendKeys("TestState");

            IWebElement zipcode = driver.FindElement(By.XPath("//div[@class='form-group w-6/12 smtablet:w-full']/input[@id='inputZip']"));
            zipcode.SendKeys("360002");

            submit.Click();

            Thread.Sleep(2000);

            IWebElement successmessage = driver.FindElement(By.XPath("//div/p[@class='success-msg hidden']"));
            string Actualmessage = successmessage.Text;
            string Expectedmessage = "Thanks for contacting us, we will get back to you shortly.";

            if (Actualmessage.Equals(Expectedmessage))
            {
                Console.WriteLine("Success message is properly appear.");
            }
            else
            {
                Console.WriteLine("Success message is not properly appear.");
            }
        }

        [TestCleanup]
        public void Cleanup()
        {
            if (driver != null)
                driver.Quit();
        }
    }
}
