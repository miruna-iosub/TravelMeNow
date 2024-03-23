
using AutoMapper;
using TravelMeNow.Application.Models.OpeningHour;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Mapping;

public class OpeningHourProfile: Profile
{
    public OpeningHourProfile()
    {
        CreateMap<OpeningHourRequestModel, OpeningHour> ();
        CreateMap<OpeningHour, OpeningHourResponseModel> ();
    }
}
