using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.MSTest;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace PlaywrightTests;

[TestClass]
public class UnitTest1 : PageTest
{
    private const string BaseUrl = "https://localhost:7063/";
    [TestMethod]
    public async Task HomepageHasPlaywrightInTitleAndGetStartedLinkLinkingtoTheIntroPage()
    {

        await Page.GotoAsync(BaseUrl);

        // Expect a title "to contain" a substring.
        await Expect(Page).ToHaveTitleAsync(new Regex("Home Page"));

        // create a locator
        var getStarted = Page.GetByRole(AriaRole.Heading, new() { Name = "Welcome" });

        var learnMore = Page.GetByRole(AriaRole.Link, new() { Name = "Learn More" });

        await Expect(getStarted).ToHaveTextAsync("Welcome");
        await Expect(getStarted).ToBeVisibleAsync();
        // Expect an attribute "to be strictly equal" to the value.
        await Expect(learnMore).ToHaveAttributeAsync("href", "https://learn.microsoft.com/en-us/aspnet/core/?view=aspnetcore-7.0");

        var privacyTab = Page.GetByTestId("privacyNav");

        await Expect(privacyTab).ToHaveAttributeAsync("href", "/Home/Privacy");

        await privacyTab.ClickAsync();

        await Expect(Page).ToHaveURLAsync($"{BaseUrl}Home/Privacy");
        // Click the get started link.
       // await getStarted.ClickAsync();

    }
}