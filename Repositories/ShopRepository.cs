using AppointmentAPI.Data;
using AppointmentAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Repositories;

public class ShopRepository : IShopRepository
{
    private readonly AppDbContext _context;

    public ShopRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<IEnumerable<Shop>> GetAllAsync()
    {
        // We Include(Category) so the frontend can see the category name
        return await _context.Shops
            .Include(s => s.Category)
            .ToListAsync();
    }

    public async Task<Shop?> GetByIdAsync(int id)
    {
        return await _context.Shops
            .Include(s => s.Category)
            .FirstOrDefaultAsync(s => s.Id == id);
    }

    public async Task AddAsync(Shop shop)
    {
        await _context.Shops.AddAsync(shop);
    }

    public async Task<bool> SaveChangesAsync()
    {
        return await _context.SaveChangesAsync() > 0;
    }
}