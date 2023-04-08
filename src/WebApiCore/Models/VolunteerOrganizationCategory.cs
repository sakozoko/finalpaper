namespace WebApiCore.Models;

public enum VolunteerOrganizationCategory
{
    Humanitarian = 1,
    BloodDonation = 2,
    HelpingChildren = 3,
    HelpingDefenders = 4,
    HelpingDisabledPeople = 5,
    HelpingElderlyPeople = 6,
    HelpingIdp = 7,
    HelpingAnimals = 8,
    FoodDistribution = 9,
    Evacuation = 10,
    Medicine = 11,
    CarVolunteering = 12,
    MentalHealth = 13,
    Doctor = 14,
    AccommodationOfIdp = 15
}

public static class VolunteerOrganizationCategoriesExtension
{
    public static string ToLink(this VolunteerOrganizationCategory category)
    {
        return category switch
        {
            VolunteerOrganizationCategory.Humanitarian => "humanitarni-shtaby",
            VolunteerOrganizationCategory.BloodDonation => "donorstvo-krovi",
            VolunteerOrganizationCategory.HelpingChildren => "dopomoha-ditiam",
            VolunteerOrganizationCategory.HelpingDefenders => "dopomoha-zakhysnykam",
            VolunteerOrganizationCategory.HelpingDisabledPeople => "dopomoha-liudiam-z-invalidnistiu",
            VolunteerOrganizationCategory.HelpingElderlyPeople => "dopomoha-starenkym",
            VolunteerOrganizationCategory.HelpingIdp => "dopomoha-pereselentsiam",
            VolunteerOrganizationCategory.HelpingAnimals => "dopomoha-tvarynam",
            VolunteerOrganizationCategory.FoodDistribution => "yizha-ta-produkty",
            VolunteerOrganizationCategory.Evacuation => "evakuatsiia-z-mista",
            VolunteerOrganizationCategory.Medicine => "liky",
            VolunteerOrganizationCategory.CarVolunteering => "poshuk-avtovolonteriv",
            VolunteerOrganizationCategory.MentalHealth => "psykholohichna-dopomoha",
            VolunteerOrganizationCategory.Doctor => "konsultatsiia-likaria",
            VolunteerOrganizationCategory.AccommodationOfIdp => "evakuatsiia-z-mista",
            _ => ""
        };
    }
}