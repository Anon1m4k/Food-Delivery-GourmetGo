using Microsoft.VisualStudio.TestTools.UnitTesting;
using OpenQA.Selenium;
using OpenQA.Selenium.Edge;
using OpenQA.Selenium.Support.UI;
using System.Diagnostics;

namespace E2ETests
{
    [TestClass]
    public class CombinedTests
    {
        private static Process _serverProcess;
        private IWebDriver _driver;
        private WebDriverWait _wait;
        private const string BaseUrl = "http://localhost:5000";
        private const string ServerPath = @"D:\Food-Delivery-GourmetGo\number system calculator KDA\bin\Debug\net7.0\number system calculator KDA.exe";


        [ClassInitialize]
        public static void ClassInitialize(TestContext context)
        {

            StartServer();
            WaitForServerReady();
        }

        [TestInitialize]
        public void Setup()
        {
            var options = new EdgeOptions();
            options.AddArgument("--headless"); // Optional headless mode
            _driver = new EdgeDriver(options);
            _driver.Manage().Window.Maximize();
            _wait = new WebDriverWait(_driver, TimeSpan.FromSeconds(15));
        }

        [TestMethod]
        public void UserLogin_Successful()
        {
            try
            {
                // Login test
                _driver.Navigate().GoToUrl($"{BaseUrl}/Account/Login");

                _wait.Until(d => d.FindElement(By.Name("Email"))).SendKeys("test@example.com");
                _wait.Until(d => d.FindElement(By.Name("Password"))).SendKeys("Qwerty123!");
                _wait.Until(d => d.FindElement(By.CssSelector("button[type='submit']"))).Click();

                // Verify redirection
                _wait.Until(d => d.Url.Contains("/Profile"));
                Assert.IsTrue(_driver.Url.Contains("/Profile"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Login test failed: {ex.Message}");
            }
        }

        [TestMethod]
        public void AddToCart_And_PlaceOrder()
        {
            try
            {
                // Login first
                UserLogin_Successful();

                // Navigate to menu
                _driver.Navigate().GoToUrl($"{BaseUrl}/UserDishs");
                _wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Добавить в корзину')]"))).Click();

                // Go to basket
                _driver.Navigate().GoToUrl($"{BaseUrl}/Basket");
                _wait.Until(d => d.FindElement(By.XPath("//button[contains(text(), 'Оформить заказ')]"))).Click();

                // Verify confirmation
                _wait.Until(d => d.Url.Contains("/OrderConfirmation"));
                Assert.IsTrue(_driver.Url.Contains("/OrderConfirmation"));
            }
            catch (Exception ex)
            {
                throw new Exception($"Order test failed: {ex.Message}");
            }
        }

        [TestCleanup]
        public void Teardown()
        {
            _driver?.Quit();
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            StopServer();
        }

        private static void StartServer()
        {
            _serverProcess = new Process
            {
                StartInfo = new ProcessStartInfo
                {
                    FileName = ServerPath,
                    UseShellExecute = true,
                    CreateNoWindow = false
                }
            };
            _serverProcess.Start();
        }

        private static void StopServer()
        {
            if (_serverProcess == null || _serverProcess.HasExited) return;

            _serverProcess.Kill();
            _serverProcess.Dispose();
            _serverProcess = null;
        }

        private static void WaitForServerReady()
        {
            var maxAttempts = 15;
            using var client = new System.Net.Http.HttpClient();

            for (var i = 0; i < maxAttempts; i++)
            {
                try
                {
                    var response = client.GetAsync($"{BaseUrl}/health").GetAwaiter().GetResult();
                    if (response.IsSuccessStatusCode) return;
                }
                catch
                {
                    Thread.Sleep(1000);
                }
            }
            throw new Exception("Server failed to start within timeout");
        }
    }
}