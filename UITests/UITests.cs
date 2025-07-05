using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.Playwright;
using Microsoft.Playwright.NUnit;
using NUnit.Framework;

namespace UITests;

[Parallelizable(ParallelScope.Self)]
[TestFixture]
public class UITests
{
    private IPlaywright _playwright = null!;
    private IBrowser _browser = null!;
    private IPage _page = null!;

    [SetUp]
    public async Task SetUp()
    {
        _playwright = await Playwright.CreateAsync();
        _browser = await _playwright.Chromium.LaunchAsync(new BrowserTypeLaunchOptions
        {
            Headless = false,
            SlowMo = 200
        });
        var context = await _browser.NewContextAsync();
        _page = await context.NewPageAsync();
    }

    [TearDown]
    public async Task TearDown()
    {
        await _browser.CloseAsync();
        _playwright.Dispose();
    }

    [Test]
    public async Task TestRequiredFields()
    {
        // Create a new Google page and search "Prometheus Group"
        var google = new GoogleSearch(_page);
        await google.SearchPrometheus("Prometheus Group");

        // Verify the search result contains "Prometheus Group"
        await _page.ScreenshotAsync(new() { Path = "search-debugging3.png", FullPage = true }); //Testing
        var prometheusLink = _page.Locator("a:has(h3:has-text('Prometheus Group'))").First;
        Assert.IsTrue(await prometheusLink.IsVisibleAsync(), "Search result does not contain 'Prometheus Group'.");

        // Click on the "Contact Us" link
        await _page.ClickAsync("text=Contact Us");
        await _page.ScreenshotAsync(new() { Path = "prometheus-debugging.png", FullPage = true }); //Testing

        // Fill in the first and last name fields on the contact page and submit
        var contact = new ContactUsPage(_page);
        await contact.FillName("Carter", "Fultz");
        await contact.Submit();

        // Validate there are 4 additional required fields
        int requiredCount = await contact.CountRequiredFields();
        Assert.That(requiredCount, Is.EqualTo(4), "Expected 4 additional required fields.");
    }


    [Test]
    public async Task TestPrometheusWebsite()
    {   
        // Go to the Prometheus Group Contact Us page
        await _page.GotoAsync("https://www.prometheusgroup.com/contact-us/");

        // Fill in the first and last name fields on the contact page and submit
        var contact = new ContactUsPage(_page);
        await contact.FillName("Carter", "Fultz");
        await contact.Submit();

        // Validate there are 4 additional required fields
        int requiredCount = await contact.CountRequiredFields();
        Assert.That(requiredCount, Is.EqualTo(4), "Expected 4 additional required fields.");
    }
}