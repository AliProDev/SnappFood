using NUnit.Framework;
using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace SnappFood
{
    class LoginPage
    {
        public IWebDriver WebDriver { get; }
        public LoginPage(IWebDriver webDriver)
        {
            WebDriver = webDriver;
        }

        #region Get_Element

        public IWebElement mobilenumber => WebDriver.FindElement(By.Id("phoneNumber-input"));
        public IWebElement submitmobile => WebDriver.FindElement(By.Id("submitPhoneNumber"));
        public IWebElement password => WebDriver.FindElement(By.Id("password-input"));
        public IWebElement submitpassword => WebDriver.FindElement(By.Id("submitPassword"));
        public IWebElement messagemobile => WebDriver.FindElement(By.Id("input-error"));
        public IWebElement messagepassword => WebDriver.FindElement(By.Id("invalid-password-error"));
        public IWebElement otppassword => WebDriver.FindElement(By.Id("input-OTP"));
        public IWebElement btnreturn => WebDriver.FindElement(By.Id("back-navBar"));
        public IWebElement btnmobileclear => WebDriver.FindElement(By.Id("back-navBar"));
        public IWebElement otpsubmitpassword => WebDriver.FindElement(By.Id("submitPasswordOTP"));
        public IWebElement otpmessagepassword => WebDriver.FindElement(By.Id("invalid-OTP-error"));
        public IWebElement firstname => WebDriver.FindElement(By.Id("firstname-input"));

        #endregion

        #region Check_InvalidMobile_ValidVerificationCode

        public void Check_Login_Step(string MobileNumber, string VerificationCode)
        {
            System.Threading.Thread.Sleep(2000);

            if (mobilenumber.GetAttribute("value") != "")
            {
                btnmobileclear.Click();
                WebDriver.Navigate().GoToUrl("https://m.snappfood.ir/login");
                System.Threading.Thread.Sleep(1000);
            }

            mobilenumber.SendKeys(MobileNumber);
            System.Threading.Thread.Sleep(500);

            if (mobilenumber.GetAttribute("value") == null && submitmobile.Enabled == true)
            {
                Console.WriteLine("No information entered but the confirmation button is active");
                Assert.Fail("No information entered but the confirmation button is active");
            }

            Regex mRegxExpression = new Regex(@"^([0-9])([0-9]*)$");
            var dsfdsf = mobilenumber.GetAttribute("value");

            string[] Persianstartdate = { "۰", "۱", "۲", "۳", "۴", "۵", "۶", "۷", "۸", "۹" };

            string[] Englishstartdate = { "0", "1", "2", "3", "4", "5", "6", "7", "8", "9" };

            for (var ii = 0; ii < 10; ii++)
            {
                dsfdsf = dsfdsf.ToString().Replace(Persianstartdate[ii], Englishstartdate[ii]);
            }

            if (mRegxExpression.IsMatch(dsfdsf.Trim()) && mobilenumber.GetAttribute("value").Count() == 11)
            {
                Console.WriteLine("The entered mobile number is correct");
            }
            else 
            {
                if (messagemobile.Text != "لطفاً شماره موبایل خود را بصورت درست وارد کنید." && submitmobile.Enabled == true)
                {
                    Console.WriteLine("Data input validation not checked");
                    Assert.Fail("Data input validation not checked");
                }

                if (mobilenumber.GetAttribute("value").Count() > 11 || mobilenumber.GetAttribute("value").Count() < 11 && submitmobile.Enabled == true)
                {
                    Console.WriteLine("Validation of incoming characters was not checked");
                    Assert.Fail("Validation of incoming characters was not checked");
                }
            }

            submitmobile.Click();
            System.Threading.Thread.Sleep(3000);

            var message = "";

            if (MobileNumber == "09301941972")
            {
                otppassword.SendKeys(VerificationCode);
                System.Threading.Thread.Sleep(500);

                if (otppassword.GetAttribute("value") == null && submitpassword.Enabled == true)
                {
                    Console.WriteLine("No information entered but the confirmation button is active");
                    Assert.Fail("No information entered but the confirmation button is active");
                }

                otpsubmitpassword.Click();
                System.Threading.Thread.Sleep(1500);

                if (MobileNumber == "09301941972" && VerificationCode == "12345")
                {
                    if (firstname.Displayed == true)
                    {
                        Console.WriteLine("Login page was checked successfully");
                    }
                    else
                    {
                        Console.WriteLine("Login page not checked successfully");
                        Assert.Fail("Login page not checked successfully");
                    }
                }
                else
                {
                    message = otpmessagepassword.Text;

                    if (MobileNumber == "09301941972" && VerificationCode != "12345" && message != "کد وارد شده نادرست است")
                    {
                        Console.WriteLine("Password validation failed");
                        Assert.Fail("Password validation failed");
                    }
                    else
                    {
                        Console.WriteLine("Password validation was performed");
                        btnreturn.Click();
                        System.Threading.Thread.Sleep(1000);
                    }
                }
            }
            else
            {
                password.SendKeys(VerificationCode);
                System.Threading.Thread.Sleep(500);

                if (password.GetAttribute("value") == null && submitpassword.Enabled == true)
                {
                    Console.WriteLine("No information entered but the confirmation button is active");
                    Assert.Fail("No information entered but the confirmation button is active");
                }

                submitpassword.Click();
                System.Threading.Thread.Sleep(1500);

                message = messagepassword.Text;

                if (MobileNumber == "09301941972" && VerificationCode != "12345" && message != "رمز عبور اشتباه است")
                {
                    Console.WriteLine("Password validation failed");
                    Assert.Fail("Password validation failed");
                }
                else
                {
                    Console.WriteLine("Password validation was performed");
                    btnreturn.Click();
                    System.Threading.Thread.Sleep(1000);
                }
            }

            

        }

        #endregion

    }
}
