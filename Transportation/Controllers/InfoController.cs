using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transportation.Services;

namespace Transportation.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InfoController : ControllerBase
{
    private readonly TransportationDbContext _context;

    public InfoController(TransportationDbContext context)
    {
        _context = context;
    }

    // GET: api/Info
    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await _context.Roles.ToListAsync());
    }
}