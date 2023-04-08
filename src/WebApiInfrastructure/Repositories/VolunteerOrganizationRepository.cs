using HtmlAgilityPack;
using WebApiApplication.Repositories;
using WebApiCore.Models;

namespace WebApiInfrastructure.Repositories;

public class VolunteerOrganizationRepository : IVolunteerOrganizationRepository
{
    private const string RepositoryUrl = "https://palyanytsya.info/";
    private readonly HtmlWeb _htmlWeb;

    public VolunteerOrganizationRepository(HtmlWeb htmlWeb)
    {
        _htmlWeb = htmlWeb;
    }

    public async Task<IEnumerable<VolunteerOrganization>> GetVolunteerOrganizationsAsync(
        VolunteerOrganizationCategory category, City city)
    {
        if (category == VolunteerOrganizationCategory.None || city.Link == null)
            return Array.Empty<VolunteerOrganization>();
        var htmlDocument =
            await _htmlWeb.LoadFromWebAsync(RepositoryUrl + "cities/" + city.Link + "/" + category.ToLink());
        var nodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='p-5 mb-4 bg-white rounded-md relative']");
        var volunteerOrganisations = nodes?.Select(h => new VolunteerOrganization
        {
            Title = h.SelectSingleNode("p[@class='mb-2 pointer-events-none -mt-32 pt-32 title w-5/6 sm:w-full']")
                ?.InnerText,
            Description = h.SelectSingleNode("p[@class='mb-3 description']/following-sibling::p[position()=1]")
                ?.InnerText,
            Phones = h.SelectNodes(
                    "div[@class='flex flex-col']/div[@class='flex flex-col text-gray-600 w-[300px]']/a[contains(@href,'tel')]/span")
                ?
                .Select(p => p.InnerText).ToArray(),
            Addresses = h.SelectNodes("div[@class='flex flex-col']/div[@class='text-gray-600 min-w-[300px]']/div/span")?
                .Select(x => x.InnerText).ToArray(),
            SocialNetworks = h.SelectNodes("div[@class='flex flex-col']/div[@class='min-w-[300px]']/div/a")?
                .Select(x => x.Attributes["href"].Value).ToArray()
        });
        return volunteerOrganisations ?? Array.Empty<VolunteerOrganization>();
    }

    public async Task<int> GetVolunteerOrganizationsCountAsync(VolunteerOrganizationCategory category, City city)
    {
        if (category == VolunteerOrganizationCategory.None || city.Link == null)
            return 0;
        var htmlDocument =
            await _htmlWeb.LoadFromWebAsync(RepositoryUrl + "cities/" + city.Link + "/" + category.ToLink());
        var nodes = htmlDocument.DocumentNode.SelectNodes("//div[@class='p-5 mb-4 bg-white rounded-md relative']");
        return nodes?.Count ?? 0;
    }

    public Task<IEnumerable<string>> GetCategoriesAsync()
    {
        return Task.FromResult<IEnumerable<string>?>(Enum.GetNames(typeof(VolunteerOrganizationCategory)))!;
    }
}