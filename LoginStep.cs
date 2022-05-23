using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TechTalk.SpecFlow;
using TechTalk.SpecFlow.Assist;

namespace SnappFood
{
    [Binding]
    [Scope(Tag = "Login")]
    public sealed class LoginStep
    {
        IWebDriver webDriver;
        LoginPage login;

        #region Launch_Application

        [Given(@"launch the application")]
        public void Launch_Application()
        {
            webDriver = new ChromeDriver();
            webDriver.Navigate().GoToUrl("https://m.snappfood.ir/login");
            login = new LoginPage(webDriver);
            webDriver.Manage().Window.Maximize();
            webDriver.Manage().Cookies.DeleteAllCookies();
            System.Threading.Thread.Sleep(2000);
        }

        #endregion

        #region Feature_Info
        public class Login_Info
        {
            public string MobileNumber { get; set; }
            public string VerificationCode { get; set; }
        }

        #endregion

        #region Check_InvalidMobile_ValidVerificationCode

        [Given(@"Check Enter invalid Mobile and valid Verification Code")]
        public void Check_InvalidMobile_ValidVerificationCode(Table table)
        {
            var info = table.CreateSet<Login_Info>();
            foreach (Login_Info item in info)
            {
                login.Check_Login_Step(item.MobileNumber, item.VerificationCode);
            }
        }

        #endregion

        #region Check_ValidMobile_InvalidVerificationCode

        [When(@"Check Enter valid Mobile and invalid Verification Code")]
        public void Check_ValidMobile_InvalidVerificationCode(Table table)
        {
            var info = table.CreateSet<Login_Info>();
            foreach (Login_Info item in info)
            {
                login.Check_Login_Step(item.MobileNumber, item.VerificationCode);
            }
        }

        #endregion

        #region Check_ValidMobile_ValidVerificationCode

        [Then(@"Enter valid Mobile and valid Verification Code")]
        public void Check_ValidMobile_ValidVerificationCode(Table table)
        {
            var info = table.CreateSet<Login_Info>();
            foreach (Login_Info item in info)
            {
                login.Check_Login_Step(item.MobileNumber, item.VerificationCode);
            }
        }

        #endregion

    }
}
