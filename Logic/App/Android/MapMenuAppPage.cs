using OpenQA.Selenium;
using OpenQA.Selenium.Appium;
using OpenQA.Selenium.Appium.Android;

public class MapMenuAppPage : AndroidAppPageBase
    {

        private string SearchInput => "com.google.android.apps.maps:id/search_omnibox_edit_text";
        private string SearchInputClickable => "//android.widget.TextView[@text='Search here']";
        private string Image => "com.google.android.apps.maps:id/street_view_thumbnail";
        private string SaveThemeBtn => "//android.view.View[@content-desc='Save']";

        public void TypeInSearchInput(string text)
        {
            Driver.FindElement(MobileBy.XPath(SearchInputClickable)).Click();
            Driver.FindElement(MobileBy.Id(SearchInput)).SendKeys(text);
            PressKeyCode(AndroidKeyCode.Enter);
        }

        public async Task<bool> WaitForImageToBeVisible(int timeout)
        {
            return await WaitUntilElementDisplayed(MobileBy.Id(Image), timeout);
        }

        public async Task<bool> WaitForSaveThemeBtnToBeVisible(int timeout)
        {
            return await WaitUntilElementDisplayed(MobileBy.XPath(SaveThemeBtn), timeout);
        }

        public void ClickSaveThemeBtn()
        {
            Driver.FindElement(MobileBy.XPath(SaveThemeBtn)).Click();
        }
    }
