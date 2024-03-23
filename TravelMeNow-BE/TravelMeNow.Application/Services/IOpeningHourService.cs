
using TravelMeNow.Application.Models.OpeningHour;

namespace TravelMeNow.Application.Services;

public interface IOpeningHourService
{
    public Task<OpeningHourResponseModel> CreateAsync(OpeningHourRequestModel openingHourRequest);
}
