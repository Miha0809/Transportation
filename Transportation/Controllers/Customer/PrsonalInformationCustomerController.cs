using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using Microsoft.EntityFrameworkCore;
using Transportation.Services;

namespace Transportation.Controllers.Customer;

[Route("api/[controller]")]
[ApiController]
public class PrsonalInformationCustomerController : Controller
{
    private readonly TransportationDbContext _context;
    private readonly int _id;
    
    public PrsonalInformationCustomerController(TransportationDbContext context)
    {
        _context = context;
        _id = int.Parse(HttpContext.User.FindFirstValue("Id"));
    }


    // GET: api/PersonalInformationCustomer/5
    [HttpGet("profile")]
    public async Task<IActionResult> PersonalInformationCustomer()
    {
        var user = await _context.Customers.FirstOrDefaultAsync(user => user.Id.Equals(_id));
        return Ok(user);
    }

    // PUT: api/PersonalInformationCustomer/5
    [HttpPut]
    public async Task<IActionResult> Edit([FromBody] Models.Customer.Customer customer)
    {
        if (customer == null)
        {
            return BadRequest();
        }

        var user = await _context.Customers.FirstOrDefaultAsync(user => user.Id.Equals(_id));

        _context.Customers.Remove(user);
        await _context.Customers.AddAsync(customer);
        await _context.SaveChangesAsync();

        return Ok();

    }

    // DELETE: api/PersonalInformationCustomer/5
    [HttpDelete]
    public async Task<IActionResult> Delete()
    {
        _context.Customers.Remove(await _context.Customers.FindAsync(_id));
        await _context.SaveChangesAsync();

        return Ok();
    }
}