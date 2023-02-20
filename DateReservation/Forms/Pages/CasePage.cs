using OpenQA.Selenium;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Aquality.Selenium.Browsers;
using Aquality.Selenium.Forms;
using Aquality.Selenium.Elements.Interfaces;
using DateReservation.Utilities;

namespace DateReservation.Forms.Pages
{
    public class CasePage: Form
    {
        private IButton ExpandPlaceDealListButton(string text) => ElementFactory.GetButton(By.XPath(string.Format("//*[contains(text(), \"{0}\")]//following-sibling:: button[contains(@class, \"btn--accordion\")]", text)), "Expand place-deal list button");

        private IButton ExpandPlacesListButton => ElementFactory.GetButton(By.XPath("//mat-label[contains(text(), \"Please select a location\")]//parent:: label"), "Expand places list button");

        private IButton ExpandDealsListButton => ElementFactory.GetButton(By.XPath("//mat-label[contains(text(), \"Please select a queue\")]//parent:: label"), "Expand deals list button");

        private IButton PlaceButton(string place) => ElementFactory.GetButton(By.XPath(string.Format("//span[contains(text(), \"{0}\")]", place)), "Place button");

        private IButton DealButton(string deal) => ElementFactory.GetButton(By.XPath(string.Format("//span[contains(text(), \"{0}\")]", deal)), "Deal button");

        private IList<ITextBox> DatesGrid => FormElement.FindChildElements<ITextBox>(By.XPath("//tbody[contains(@class, \"mat-calendar-body\")]//td[@role = \"gridcell\"]"), "Dates grid");

        //private ITextBox DisabledDate => ElementFactory.GetTextBox(By.XPath("//tbody[contains(@class, \"mat-calendar-body\")]//td[contains(@class, \"mat-calendar-body-disabled\")]"), "Disabled dates");

        private IButton Spinner => ElementFactory.GetButton(By.XPath("//div[contains(@class, \"spinner\")] "), "Spinner");


        


        public CasePage() : base(By.XPath("//div[@class = \"cases__content\"]"), "Case page")
        {
        }

        public int SelectDate()
        {
            int activeDates = 0;

            Spinner.State.WaitForEnabled();
            Spinner.State.WaitForNotExist();

            foreach (var dateElement in DatesGrid)
            {
                if (dateElement.GetAttribute("aria-disabled") != "true")
                {
                    activeDates++;
                    dateElement.Click();
                    Spinner.State.WaitForEnabled();
                    Spinner.State.WaitForNotExist();
                    //select date
                }
            }

            return activeDates;
        }



        public void SelectPlaceAndDeal(string place, string deal, string listText)
        {
            ExpandPlaceDealListButton(listText).State.WaitForEnabled();
            ExpandPlaceDealListButton(listText).Click();

            SelectPlace(place);
            SelectDeal(deal);
        }

        private void SelectPlace(string place)
        {
            ExpandPlacesListButton.State.WaitForEnabled();
            ExpandPlacesListButton.JsActions.Click();

            PlaceButton(place).State.WaitForEnabled();
            PlaceButton(place).Click();
        }

        private void SelectDeal(string deal)
        {
            ExpandDealsListButton.State.WaitForEnabled();
            ExpandDealsListButton.JsActions.Click();

            DealButton(deal).State.WaitForEnabled();
            DealButton(deal).Click();
        }
    }
}
