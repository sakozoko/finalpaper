using WebApiCore.Models;

namespace WebApiApplication.Repositories;

public interface IVolunteerOrganizationRepository
{
    public Task<IEnumerable<VolunteerOrganization>> GetVolunteerOrganizationsAsync(
        VolunteerOrganizationCategory category, City city);

    public Task<IEnumerable<string>> GetCategoriesAsync();
    public Task<int> GetVolunteerOrganizationsCountAsync(VolunteerOrganizationCategory category, City city);
}