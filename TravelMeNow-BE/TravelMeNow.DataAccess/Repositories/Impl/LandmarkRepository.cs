using TravelMeNow.DataAccess.Entities;
using TravelMeNow.DataAccess.Persistence;
using Microsoft.EntityFrameworkCore;

namespace TravelMeNow.DataAccess.Repositories.Impl;

public class LandmarkRepository : ILandmarkRepository
{
    private readonly DatabaseContext _databaseContext;

    public LandmarkRepository(DatabaseContext databaseContext)
    {
        _databaseContext = databaseContext;
    }

    public async Task<Landmark> CreateAsync(Landmark landmark)
    {
        var addedLandmark = (await _databaseContext.Landmarks.AddAsync(landmark)).Entity;
        await _databaseContext.SaveChangesAsync();
        return addedLandmark;
    }

    public async Task<Landmark?> GetByNameAsync(string name)
    {
        var landmark = await _databaseContext.Landmarks.Include(point => point.Schedules).Where(point => point.Name == name).FirstOrDefaultAsync();
        return landmark;
    }

    public async Task<IEnumerable<Landmark>> GetAllAsync()
    {
        var landmarks = await _databaseContext.Landmarks.Include(point => point.Schedules).ToListAsync();

        return landmarks;
    }
}
