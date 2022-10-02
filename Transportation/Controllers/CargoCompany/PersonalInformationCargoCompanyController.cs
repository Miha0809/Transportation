using System.Security.Claims;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Transportation.Services;

namespace Transportation.Controllers.CargoCompany;

[Route("api/[controller]")]
[ApiController]
public class PersonalInformationCargoCompanyController : ControllerBase
{
    private readonly TransportationDbContext _context;
    private readonly int _id;
    
    public PersonalInformationCargoCompanyController(TransportationDbContext context)
    {
        _context = context;
        _id = int.Parse(HttpContext.User.FindFirstValue("Id"));
    }
    

    // GET: api/PersonalInformationCargoCompany/5
    [HttpGet("profile")]
    public async Task<IActionResult> PersonalInformationCargoCompany()
    {
        var user = await _context.CargoCompanies.FirstOrDefaultAsync(user => user.Id.Equals(_id));
        return Ok(user);
    }
    
    // PUT: api/PersonalInformationCargoCompany/5
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] Models.CargoCompany.CargoCompany cargoCompany)
    {
        if (cargoCompany == null)
        {
            return BadRequest();
        }

        var company = await _context.CargoCompanies.FirstOrDefaultAsync(user => user.Id.Equals(_id));

        _context.CargoCompanies.Remove(company);
        await _context.CargoCompanies.AddAsync(cargoCompany);
        await _context.SaveChangesAsync();

        return Ok();

    }

    // DELETE: api/PersonalInformationCargoCompany/5
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        _context.CargoCompanies.Remove(await _context.CargoCompanies.FindAsync(_id));
        await _context.SaveChangesAsync();

        return Ok();
    }
}