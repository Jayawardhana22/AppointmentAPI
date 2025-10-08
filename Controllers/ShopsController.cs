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
        var shop = _mapper.Map<Shop>(shopDto);
        shop.OwnerId = int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

        await _shopRepository.AddAsync(shop);
        await _shopRepository.SaveChangesAsync();

        return CreatedAtAction(nameof(GetById), new { id = shop.Id }, _mapper.Map<ShopDto>(shop));
    }
}