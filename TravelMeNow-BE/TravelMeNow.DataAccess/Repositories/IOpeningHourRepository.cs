using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.DataAccess.Repositories;

public interface IOpeningHourRepository
{
    Task<OpeningHour> CreateAsync(OpeningHour openingHour);
}
