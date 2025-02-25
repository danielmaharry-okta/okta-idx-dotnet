﻿using embedded_auth_with_sdk.E2ETests.Drivers;
using OpenQA.Selenium;

namespace embedded_auth_with_sdk.E2ETests.PageObjectModels
{
    public class SelectAuthenticatorPage : BasePage
    {
        public override string RelativePageUri => "Manage/SelectAuthenticator";
        public IWebElement PasswordAuthenticator => _webDriver.FindElement(By.XPath("//label[contains(normalize-space(.),\"Password\")]"));
        public IWebElement EmailAuthenticator => _webDriver.FindElement(By.XPath("//label[contains(normalize-space(.),\"Email\")]"));
        public IWebElement PhoneAuthenticator => _webDriver.FindElement(By.XPath("//label[contains(normalize-space(.),\"Phone\")]"));

        public IWebElement GoogleAuthenticator => _webDriver.FindElement(By.XPath("//label[contains(normalize-space(.),\"Google Authenticator\")]"));
        public IWebElement SecurityQuestion => _webDriver.FindElement(By.XPath("//label[contains(normalize-space(.),\"Security Question\")]"));
        public IWebElement SubmitButton => _webDriver.FindElement(By.XPath("//input[@Value=\"Submit\"]"));
        public IWebElement SkipThisStepButton => _webDriver.FindElement(By.XPath("//input[@name=\"skip\"]"));

        public SelectAuthenticatorPage(WebDriverDriver webDriverDriver, ITestConfiguration testConfiguration)
            : base(webDriverDriver, testConfiguration)
        {

        }        
    }
}
