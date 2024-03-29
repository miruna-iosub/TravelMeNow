
using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Persistence;

namespace TravelMeNow.DataAccess.Repositories.Impl;

public class ScheduleRepository : IScheduleRepository
{
    private readonly DatabaseContext _databaseContext;

    public ScheduleRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Schedule> CreateAsync(Schedule schedule)
    {
        var addedSchedule = (await _databaseContext.Schedules.AddAsync(schedule)).Entity;
        await _databaseContext.SaveChangesAsync();
        return addedSchedule;
    }
}
