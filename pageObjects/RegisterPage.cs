using OpenQA.Selenium;
using SeleniumExtras.PageObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenQA.Selenium.Support.UI;
using System.Threading;

namespace NetwealthNunit.pageObjects
{
    public class RegisterPage
    {
        public string email1;
        //public String email1;
        private IWebDriver driver;
        public RegisterPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        //pageobject factory
        [FindsBy(How = How.Id, Using = "FirstName")]
        private IWebElement firstName;

        [FindsBy(How = How.Id, Using = "LastName")]
        private IWebElement lastName;

        [FindsBy(How = How.Id, Using = "Email")]
        private IWebElement email;

        [FindsBy(How = How.Id, Using = "Password")]
        private IWebElement password;

        [FindsBy(How = How.Id, Using = "ReferralSource")]
        private IWebElement hearAbout;

        [FindsBy(How = How.Id, Using = "HasOptedOutOfMarketingMaterial")]
        private IWebElement marketingCheckbox;

        [FindsBy(How = How.XPath, Using = "//button[contains(text(),'Try')]")]
        private IWebElement registerbutton;

        [FindsBy(How = How.Id, Using = "jAccept")]
        private IWebElement acceptpopup;

        [FindsBy(How = How.XPath, Using = "//h1[contains(text(),'Thank you')]")]
        private IWebElement registerverification;

        [FindsBy(How = How.Id, Using = "Email-error")]
        private IWebElement emailError;

        [FindsBy(How = How.Id, Using = "FirstName-error")]
        private IWebElement fnameError;

        [FindsBy(How = How.Id, Using = "LastName-error")]
        private IWebElement lnameError;

        [FindsBy(How = How.Id, Using = "Password-error")]
        private IWebElement passwordError;

        [FindsBy(How = How.Id, Using = "ReferralSource-error")]
        private IWebElement heraAboutError;


        //to access webelement

        public void ClickAccept()
        {
            acceptpopup.Click();
        }
        public void SetFirstNameText(string firstNameVariable)
        {
            firstName.Clear();
            firstName.SendKeys(firstNameVariable);
        }

        public void SetLastNameText(string lastNameVariable)
        {
            lastName.Clear();
            lastName.SendKeys(lastNameVariable);
        }

        public void EnterEmail(string emailstr)
        { 
            email.Clear();
            email.SendKeys(emailstr);
        }

        public void EnterPassword(string passwordVariable)
        {
            password.Clear();
            password.SendKeys(passwordVariable);
        }

        public void SetHearAboutText(string countryString)
        {
            hearAbout.Click();
            hearAbout.SendKeys(countryString);
            hearAbout.SendKeys(Keys.Return);
            
        }

        public void ClickMarketingCheckbox()
        {
            marketingCheckbox.Click();
        }

        public void ClickCreateAccount()
        {
            registerbutton.Click();
        }
        
        public string GetRegistrationverificationText()
        {
            return registerverification.Text; 
        }

        public string emailValidationError()
        {
            return emailError.Text;

        }

        public string fnameValidationError()
        {
            return fnameError.Text;

        }

        public string lnameValidationError()
        {
            return lnameError.Text;

        }

        public string passwordValidationError()
        {
            return passwordError.Text;

        }

        public string hearAboutValidationError()
        {
            return heraAboutError.Text;

        }

        public string verifyPageTitle()
        {
           return driver.Title;
        }

        // Param:  
        // Method to call Gmail API to get access code
        // return 
        //public string CallGmailAPI(string fromMail)
        //{
        //    try
        //    { 
        //   new WebDriverWait(driver, TimeSpan.FromSeconds(20)).Until(ExpectedConditions.ElementExists(By.Id("ematgil_info")));

        //    }
        //    catch
        //    {
        //        Console.WriteLine("I am in test page");
        //    }
        //    Console.WriteLine("I am in test page");
        //    return GoogleAPI.GetNewUnreadEmail(fromMail).Result;
        //}

    }
}
