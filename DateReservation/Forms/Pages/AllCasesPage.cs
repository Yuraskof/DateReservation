using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquality.Selenium.Elements.Interfaces;
using Aquality.Selenium.Forms;
using OpenQA.Selenium;

namespace DateReservation.Forms.Pages
{
    public class AllCasesPage: Form
    {
        private IButton CaseButton => ElementFactory.GetButton(By.XPath("//td[contains(text(),  \" Recognition as a Polish citizen \")]"), "Case button");


        public AllCasesPage() : base(By.XPath("//div[@class = \"cases__table\"]"), "Cases page")
        {
        }

        public void GoToCitizenshipCase()
        {
            CaseButton.State.WaitForEnabled();
            CaseButton.JsActions.ScrollIntoView();
            CaseButton.Click();
        }
    }
}
