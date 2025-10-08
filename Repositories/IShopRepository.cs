using AppointmentAPI.Models;

namespace AppointmentAPI.Repositories;

public interface IShopRepository
{
    Task<IEnumerable<Shop>> GetAllAsync();
    Task<Shop?> GetByIdAsync(int id);
    Task AddAsync(Shop shop);
    Task<bool> SaveChangesAsync();
}