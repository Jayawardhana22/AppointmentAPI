using AppointmentAPI.DTOs;
using AppointmentAPI.Models;
using AppointmentAPI.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace AppointmentAPI.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ShopsController : ControllerBase
{
    private readonly IShopRepository _shopRepository;
    private readonly IMapper _mapper;

    public ShopsController(IShopRepository shopRepository, IMapper mapper)
    {
        _shopRepository = shopRepository;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var shops = await _shopRepository.GetAllAsync();
        var shopDtos = _mapper.Map<IEnumerable<ShopDto>>(shops);
        return Ok(shopDtos);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var shop = await _shopRepository.GetByIdAsync(id);
        if (shop == null) return NotFound();

        var shopDto = _mapper.Map<ShopDto>(shop);
        return Ok(shopDto);
    }

    [Authorize(Roles = "Owner")]
    [HttpPost]
    public async Task<IActionResult> Create(ShopDto shopDto)
    {
        // --- 1. SAFER USER ID CHECK ---
        // Try to get the ID from different common claim names
        var userIdString = User.FindFirst(ClaimTypes.NameIdentifier)?.Value 
                           ?? User.FindFirst("id")?.Value 
                           ?? User.FindFirst("sub")?.Value;

        if (string.IsNullOrEmpty(userIdString))
        {
            return Unauthorized("User ID not found in token. Please log out and log in again.");
        }

        if (!int.TryParse(userIdString, out int ownerId))
        {
            return BadRequest("Invalid User ID format.");
        }

        // --- 2. MAP AND ASSIGN ---
        var shop = _mapper.Map<Shop>(shopDto);
        shop.OwnerId = ownerId; // Force the owner to be the logged-in user

        // --- 3. SAVE ---
        await _shopRepository.AddAsync(shop);
        
        // This saves the changes to the DB
        // If your Repo AddAsync already has SaveChanges, you can remove this line.
        // But based on your previous code, you need this:
        await _shopRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = shop.Id }, _mapper.Map<ShopDto>(shop));
    }
}