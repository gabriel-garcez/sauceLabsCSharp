using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using NUnit.Framework.Interfaces;
using OpenQA.Selenium;
using OpenQA.Selenium.Remote;

namespace Selenium3.Nunit.Scripts.OnboardingTests
{
    [TestFixture]
    [Category("InstantSauceTest"), Category("NUnit"), Category("Instant")]
    [Parallelizable]
    public class InstantSauceTest4
    {
        private IWebDriver _driver;
        private readonly string _sauceUserName =
            Environment.GetEnvironmentVariable("PUT_HERE_YOUR USERNAME", EnvironmentVariableTarget.User);
        private readonly string _sauceAccessKey =
            Environment.GetEnvironmentVariable("PUT_HERE_YOUR ACCESS KEY", EnvironmentVariableTarget.User);
        private IJavaScriptExecutor _javascriptExecutor;

        [Test]
        public void BestPractices()
        {
            _javascriptExecutor.ExecuteScript("sauce:context=Open SauceDemo.com");
            _driver.Navigate().GoToUrl("https://www.saucedemo.com");

            _javascriptExecutor.ExecuteScript("sauce:context=Sleep for 10000ms");
            Thread.Sleep(10000);
            Assert.IsTrue(true);
        }

        [SetUp]
        [Obsolete]
        public void ExecuteBeforeEveryTest()
        {
            DesiredCapabilities caps = new DesiredCapabilities();
            caps.SetCapability("browserName", "Safari");
            caps.SetCapability("platform", "macOS 10.13");
            caps.SetCapability("version", "11.1");
            caps.SetCapability("username", _sauceUserName);
            caps.SetCapability("accessKey", _sauceAccessKey);
            caps.SetCapability("name", TestContext.CurrentContext.Test.Name);

            var tags = new List<string> { "demoTest", "sauceDemo" };
            caps.SetCapability("tags", tags);


            _driver = new RemoteWebDriver(new Uri("http://ondemand.saucelabs.com:80/wd/hub"),
                caps, TimeSpan.FromSeconds(600));

            _javascriptExecutor = ((IJavaScriptExecutor)_driver);

            _driver.Url = ("https://google.com/");
            _driver.FindElement(By.Name("q")).SendKeys("Tata Consultancy Services");
            _driver.FindElement(By.Name("btnK")).Click();

        }


        [TearDown]
        public void CleanUpAfterEveryTestMethod()
        {
            var passed = TestContext.CurrentContext.Result.Outcome.Status == TestStatus.Passed;
            _javascriptExecutor.ExecuteScript("sauce:job-result=" + (passed ? "passed" : "failed"));
            _driver?.Quit();
        }
    }
}