using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using DateReservation.Constants;
using DateReservation.Utilities;
using OpenQA.Selenium;

namespace DateReservation.Forms.Pages
{
    public class LoginPage : Form
    {
        private IButton AcceptCookiesButton => ElementFactory.GetButton(By.XPath("//button[@action-type= 'ACCEPT']"), "Accept cookies button");
        private IButton LoginButton => ElementFactory.GetButton(By.XPath("//div[@class = \"login__content\"]//button[contains (@class, \"btn--submit\")]"), "Login button");
        private ITextBox LoginTextBox => ElementFactory.GetTextBox(By.XPath("//div[@class = \"form__field\"]//input[@type = \"email\"]"), "Login text box");
        private ITextBox PasswordTextBox => ElementFactory.GetTextBox(By.XPath("//div[@class = \"form__field\"]//input[@type = \"password\"]"), "Password text box");
        private ITextBox ErrorMessage => ElementFactory.GetTextBox(By.XPath("//div[contains(@class, \"message--error\")] "), "Error message");

        public TimeSpan LoginTimeout = new TimeSpan(0,0,0, Convert.ToInt32(FileUtils.TestData.LoginTimeoutSeconds));

        public LoginPage() : base(By.XPath("//div[@class = \"login__content\"]"), "Login page")
        {
        }
        
        public void AcceptCookies()
        {
            AcceptCookiesButton.State.WaitForEnabled();
            AcceptCookiesButton.Click();
        }

        public void Login(string login, string password)
        {
            FillCreds(login, password);

            LoginButton.State.WaitForEnabled();
            LoginButton.Click();

            while (true)
            {
                try
                {
                    FormElement.State.WaitForNotDisplayed(LoginTimeout);
                    break;
                }
                catch (Exception e)
                {
                    LoginButton.Click();
                }
            }
        }

        private void FillCreds(string login, string password)
        {
            LoginTextBox.State.WaitForEnabled();
            LoginTextBox.SendKeys(login);

            PasswordTextBox.State.WaitForEnabled();
            PasswordTextBox.SendKeys(password);

        }
    }
}