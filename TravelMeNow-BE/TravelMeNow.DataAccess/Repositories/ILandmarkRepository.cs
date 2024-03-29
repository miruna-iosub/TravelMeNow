
using TravelMeNow.DataAccess.Entities;

namespace TravelMeNow.DataAccess.Repositories;

public interface ILandmarkRepository
{
    Task<Landmark> CreateAsync(Landmark landmark);

    Task<Landmark?> GetByNameAsync(string name);
    
    Task<IEnumerable<Landmark>> GetAllAsync();
}
