using Microsoft.Playwright;

public class GoogleSearch
{
    private readonly IPage _page;

    public GoogleSearch(IPage page)
    {
        _page = page;
    }

    public async Task SearchPrometheus(string query)
    {
        // Go to Google.com
        await _page.GotoAsync("http://www.google.com/");

        // Input "Prometheus Group" into the search bar
        // await _page.Locator("textarea[name='q']").FillAsync(query);
        await _page.FillAsync("textarea[name='q']", query);
        await _page.ScreenshotAsync(new() { Path = "google-debugging.png", FullPage = true }); //Testing

        // Click the Google Search button to search results
        // await _page.Locator("textarea[name='q']").PressAsync("Enter");
        // await _page.PressAsync("textarea[name='q']", "Enter");
        await _page.ClickAsync("input[value='Google Search']");
        await _page.ScreenshotAsync(new() { Path = "search-debugging1.png", FullPage = true }); //Testing

        // await _page.WaitForSelectorAsync("h3");
        await _page.ScreenshotAsync(new() { Path = "search-debugging2.png", FullPage = true }); //Testing
    }
}