using NetwealthNunit.utilities;
using NetwealthNunit.pageObjects;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NetwealthNunit.Tests
{
    public class Register : Base
    {
        public string email1;

        //generate unique email
        public string generateuniqueEmail()
        {
            string Month = DateTime.Now.ToString("MMM");
            string Date = DateTime.Now.ToString("dd");
            string year = DateTime.Now.ToString("yyyy");
            DateTime d = DateTime.Now;
            string dateString = d.ToString("yyyyMMddHHmmss");
            string email1 = "ddhanashrichavan+" + dateString + "@gmail.com";
            return this.email1 = email1;

        }


        [Test]
        public void ValidRegistration1()
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.SetFirstNameText("Dhana");
            registerpage.SetLastNameText("Chavan");
            registerpage.EnterEmail(generateuniqueEmail());
            registerpage.EnterPassword("Test@123");
            registerpage.SetHearAboutText("Event");
            registerpage.ClickCreateAccount();
            // To Verify the registration success message
            Assert.AreEqual("Thank you for registering", registerpage.GetRegistrationverificationText(), "Expected 'Email verification' in header");

        }


        [Test]
        public void ValidRegistration2()
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.SetFirstNameText("Dhana");
            registerpage.SetLastNameText("Chavan");
            registerpage.EnterEmail(generateuniqueEmail());
            registerpage.EnterPassword("Test@123");
            registerpage.SetHearAboutText("Event");
            registerpage.ClickMarketingCheckbox();
            registerpage.ClickCreateAccount();
            // To Verify the registration success message
            Assert.AreEqual("Thank you for registering", registerpage.GetRegistrationverificationText(), "Expected 'Email verification' in header");

        }
    }

}
