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
    public class InvalidRegisterCases : Base
    {
        //register without entering an email
        [Test, TestCaseSource("AddTestDataConfig")]
        public void inValidRegistration(string firstname, string lastname, string password)
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.SetFirstNameText(firstname);
            registerpage.SetLastNameText(lastname);
            registerpage.EnterPassword(password);
            registerpage.SetHearAboutText("Event");
            registerpage.ClickMarketingCheckbox();
            registerpage.ClickCreateAccount();
            // To Verify the email validation error message
            Assert.AreEqual("Please provide an email address", registerpage.emailValidationError(), "Expected 'Please provide an email address' in header");
        }

        //register with all empty fields
        [Test]
        public void inValidRegistration()
        {
            RegisterPage registerpage = new RegisterPage(getDriver());
            registerpage.ClickAccept();
            registerpage.ClickCreateAccount();
            // To Verify the email validation error message
            Assert.AreEqual("Please provide an email address", registerpage.emailValidationError(), "Expected 'an email address' in header");

            // To Verify the fname validation error message
            Assert.AreEqual("Please provide your first name", registerpage.fnameValidationError(), "Expected 'firstname' in header");

            // To Verify the lname validation error message
            Assert.AreEqual("Please provide your last name", registerpage.lnameValidationError(), "Expected 'lastname' in header");

            // To Verify the password validation error message
            Assert.AreEqual("Please enter a valid password", registerpage.passwordValidationError(), "Expected 'password' in header");

            // To Verify the hearabout validation error message
            Assert.AreEqual("Please tell us where you heard about Netwealth", registerpage.hearAboutValidationError(), "Expected 'hearabout' in header");
        }


        public static IEnumerable<TestCaseData> AddTestDataConfig()
        {

            yield return new TestCaseData(getDataParser().extractData("firstname"), getDataParser().extractData("lastname"), getDataParser().extractData("password"));
        }
    }
}
