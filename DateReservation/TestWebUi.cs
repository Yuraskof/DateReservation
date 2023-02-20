using Aquality.Selenium.Browsers;
using DateReservation.Base;
using DateReservation.Forms;
using DateReservation.Forms.Pages;
using DateReservation.Utilities;

namespace DateReservation
{
    public class TestWebUi:BaseTest
    {
        [Test(Description = "ET-0001 Checking the date reservation UI")]
        public void TestWebUiAndDatabase()
        {
            AqualityServices.Browser.GoTo(FileUtils.TestData.Url);

            LoginPage loginPage = new();
            Assert.IsTrue(loginPage.State.WaitForDisplayed(), $"{loginPage.Name} should be presented");
            
            loginPage.Login(FileUtils.LoginUser.Login, FileUtils.LoginUser.Password);

            MainNavigationForm mainNavigationForm = new MainNavigationForm();

            mainNavigationForm.GoToCases();

            AllCasesPage allCasesPage = new AllCasesPage();

            allCasesPage.GoToCitizenshipCase();

            CasePage casePage = new CasePage();

            casePage.SelectPlaceAndDeal(FileUtils.TestData.Place, FileUtils.TestData.Deal, FileUtils.TestData.ExpandPlacesDealsList);

            while (true) // send notific - stop
            {
                int activeDates = casePage.SelectDate();

                if (activeDates == 0)
                {
                    AqualityServices.Browser.Refresh();
                    casePage.SelectPlaceAndDeal(FileUtils.TestData.Place, FileUtils.TestData.Deal, FileUtils.TestData.ExpandPlacesDealsList);
                }
                else
                {
                    // next page
                }
            }
        }
    }
}