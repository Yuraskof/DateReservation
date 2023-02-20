using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace DateReservation.Forms
{
    public class MainNavigationForm : Form
    {
        private IButton AllCasesButton => ElementFactory.GetButton(By.XPath("//header[@class = \"header\"]//a[@routerlink = \"home/cases\"] "), "All cases button");
        
        public MainNavigationForm() : base(By.XPath("//header[@class = \"header\"]"), "Main navigation form") 
        {
        }

        public void GoToCases()
        {
            AllCasesButton.State.WaitForEnabled();
            AllCasesButton.Click();
        }
    }
}
