using Microsoft.Playwright;

public class ContactUsPage
{
    private readonly IPage _page;

    public ContactUsPage(IPage page)
    {
        _page = page;
    }

    public async Task FillName(string firstname, string lastname)
    {
        await _page.FillAsync("input[name='first_name']", firstname);
        await _page.FillAsync("input[name='last_name']", lastname);
    }

    public async Task Submit()
    {
        await _page.ClickAsync("button:has-text('Submit')");
    }

    public async Task<int> CountRequiredFields()
    {
        // Select fields with the required attribute that are empty
        var requiredFields = await _page.Locator("[required]").AllAsync();
        return requiredFields.Count;
    }
}