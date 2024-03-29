using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.DataAccess.Repositories;

public interface IScheduleRepository
{
    Task<Schedule> CreateAsync(Schedule schedule);
}
