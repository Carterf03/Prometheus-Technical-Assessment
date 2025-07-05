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
        await _page.FillAsync("input[name='firstname']", firstname);
        await _page.FillAsync("input[name='lastname']", lastname);
    }

    public async Task Submit()
    {
        await _page.ClickAsync("input:has-text('Contact Us')");
    }

    public async Task<int> CountRequiredFields()
    {
        // Select fields with the required attribute
        var requiredFields = _page.Locator("[required]");
        int requiredCount = 0;

        // Iterate over the required field to check if they are empty
        for (int i = 0; i < await requiredFields.CountAsync(); i++)
        {
            var field = requiredFields.Nth(i);
            var value = await field.InputValueAsync();

            if (string.IsNullOrWhiteSpace(value))
            {
                requiredCount++;
            }
        }

        return requiredCount;
    }
}