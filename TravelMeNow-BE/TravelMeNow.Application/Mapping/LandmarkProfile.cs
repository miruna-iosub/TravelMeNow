using AutoMapper;
using TravelMeNow.Application.Models.Landmark;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Mapping;

public class LandmarkProfile : Profile
{
    public LandmarkProfile()
    {
        this.CreateMap<Landmark, LandmarkResponseModel>();
        this.CreateMap<LandmarkRequestModel, Landmark>();

    }
}
