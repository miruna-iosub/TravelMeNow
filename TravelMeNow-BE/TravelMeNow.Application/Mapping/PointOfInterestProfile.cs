using AutoMapper;
using TravelMeNow.Application.Models.PointOfInterest;
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.Application.Mapping;

public class PointOfInterestProfile : Profile
{
    public PointOfInterestProfile()
    {
        this.CreateMap<PointOfInterest, PointOfInterestResponseModel>();
        this.CreateMap<PointOfInterestRequestModel, PointOfInterest>();

    }
}
