using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Threading;

namespace SeleniumApp
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                ChromeOptions options = new ChromeOptions();
                options.SetLoggingPreference(LogType.Browser, LogLevel.Warning);

                ChromeDriver driver = new ChromeDriver(options);
                driver.Manage().Window.Maximize();
                driver.Manage().Cookies.DeleteAllCookies();               
                driver.Url = "http://localhost:61685/";

                var nav = driver.Navigate();
                nav.GoToUrl("http://localhost:61685/");

                Login(driver);
                RecetasProduccion(nav, driver);

                var log = driver.Manage().Logs;
                var entries = log.GetLog(LogType.Browser);
                foreach (var entry in entries)
                {
                    Console.WriteLine(entry.ToString());
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(String.Format("ERROR: {0}", ex.ToString()));
            }
        }

        private static void RecetasProduccion(INavigation nav, ChromeDriver driver)
        {
            nav.GoToUrl("http://localhost:61685/Configuracion/RecetasProduccion");

            IWebElement lanzarButton = driver.FindElement(By.Id("btnIniciar1"));
            IWebElement infoButton = driver.FindElement(By.Id("btnInfo38"));
            IWebElement pauseButton = driver.FindElement(By.Id("btnPausar38"));
            IWebElement stopButton = driver.FindElement(By.Id("btnDetener38"));
            IWebElement archivarButton = driver.FindElement(By.Id("btnArchivar38"));

            // Archivar 
            archivarButton.Click();
            Thread.Sleep(2000);
            IWebElement confirmarArchivar = driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
            confirmarArchivar.Click();
            Thread.Sleep(2000);
            IWebElement confirmarOk = driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
            confirmarOk.Click();
            Thread.Sleep(2000);

            // Lanzar
            lanzarButton.Click();
            Thread.Sleep(4000);
            driver.FindElement(By.XPath(".//*[@id='amasadora']/option[3]")).Click();
            driver.FindElement(By.XPath(".//*[@id='tipoProducto']/option[3]")).Click();
            driver.FindElement(By.XPath(".//*[@id='tipoFormato']/option[3]")).Click();
            IWebElement crearButton = driver.FindElement(By.Id("lanzaRecetaBtn"));
            crearButton.Click();
            Thread.Sleep(200);
            IWebElement confirmarOk2 = driver.FindElement(By.XPath("//button[@class='swal2-confirm swal2-styled']"));
            confirmarOk2.Click();
            Thread.Sleep(2000);
        }

        private static void Login(ChromeDriver driver)
        {
            IWebElement usuarioTextBox = driver.FindElement(By.Id("usuario"));
            IWebElement passwordTextBox = driver.FindElement(By.Id("password"));
            IWebElement loginButton = driver.FindElement(By.TagName("button"));

            usuarioTextBox.SendKeys("Ansotec");
            passwordTextBox.SendKeys("B18822189b");
            loginButton.Click();
        }
    }
}
