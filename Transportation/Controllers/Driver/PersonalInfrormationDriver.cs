using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Transportation.Services;

namespace Transportation.Controllers.Driver;

[Route("api/[controller]")]
[ApiController]
public class PrsonalInformationDriverController : Controller
{
    private readonly TransportationDbContext _context;
    private readonly int _id;

    public PrsonalInformationDriverController(TransportationDbContext context)
    {
        _context = context;
        _id = int.Parse(HttpContext.User.FindFirstValue("Id"));
    }


    // GET: api/PersonalInformationDriver/5
    [HttpGet("profile")]
    public async Task<IActionResult> PersonalInformationDriver()
    {
        var user = await _context.Drivers.FirstOrDefaultAsync(user => user.Id.Equals(_id));
        return Ok(user);
    }

    // PUT: api/PersonalInformationDriver/5
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] Models.Driver.Driver Driver)
    {
        if (Driver == null)
        {
            return BadRequest();
        }

        var user = await _context.Drivers.FirstOrDefaultAsync(user => user.Id.Equals(_id));

        _context.Drivers.Remove(user);
        await _context.Drivers.AddAsync(Driver);
        await _context.SaveChangesAsync();

        return Ok();

    }

    // DELETE: api/PersonalInformationDriver/5
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        _context.Drivers.Remove(await _context.Drivers.FindAsync(_id));
        await _context.SaveChangesAsync();

        return Ok();
    }
}