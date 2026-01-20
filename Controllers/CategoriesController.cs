using AppointmentAPI.Models;
using AppointmentAPI.Data;   // <-- add this

using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AppointmentAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CategoriesController : ControllerBase
{
    private readonly AppDbContext _ctx;
    public CategoriesController(AppDbContext ctx) => _ctx = ctx;

    [HttpGet]
public async Task<ActionResult<List<Category>>> GetAll()
{
    var list = await _ctx.Categories.OrderBy(c => c.Name).ToListAsync();
    Console.WriteLine($">>> Categories returned: {list.Count}");
    return list;
}
}