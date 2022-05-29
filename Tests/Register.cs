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
    [Parallelizable(ParallelScope.Self)]
    public class Register : Base
    {
        public string email1;

        //validate page title and registration page
        [Test]
        public void validateregistrationpagetitle()
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            Assert.AreEqual("Online Wealth Management and Investment Services | Netwealth", registerpage.verifyPageTitle(), "Expected 'page title' in header");
        }
        //Register with different data  2 with valid data, 1 with incorrect password
        [Test, TestCaseSource("AddTestDataConfig")]
        [Parallelizable(ParallelScope.All)]
        public void ValidRegistration1(string firstname, string lastname, string password)
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.SetFirstNameText(firstname);
            registerpage.SetLastNameText(lastname);
            registerpage.EnterEmail(generateuniqueEmail());
            registerpage.EnterPassword(password);
            registerpage.SetHearAboutText("Event");
            registerpage.ClickCreateAccount();
            // To Verify the registration success message
            Assert.AreEqual("Thank you for registering", registerpage.GetRegistrationverificationText(), "Expected 'Email verification' in header");

        }

        // register with marketing checkbox
        [Test, TestCaseSource("AddTestDataConfig")]
        [Parallelizable(ParallelScope.All)]
        public void ValidRegistration2(string firstname, string lastname, string password)
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.SetFirstNameText(firstname);
            registerpage.SetLastNameText(lastname);
            registerpage.EnterEmail(generateuniqueEmail());
            registerpage.EnterPassword(password);
            registerpage.SetHearAboutText("Event");
            registerpage.ClickMarketingCheckbox();
            registerpage.ClickCreateAccount();
            // To Verify the registration success message
            Assert.AreEqual("Thank you for registering", registerpage.GetRegistrationverificationText(), "Expected 'Please provide an email address' in header");

        }

        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {

            yield return new TestCaseData(getDataParser().extractData("firstname"), getDataParser().extractData("lastname"), getDataParser().extractData("password"));
            yield return new TestCaseData(getDataParser().extractData("firstname1"), getDataParser().extractData("lastname1"), getDataParser().extractData("password1"));
            yield return new TestCaseData(getDataParser().extractData("firstname2"), getDataParser().extractData("lastname2"), getDataParser().extractData("password2"));
        }

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
    }

}
