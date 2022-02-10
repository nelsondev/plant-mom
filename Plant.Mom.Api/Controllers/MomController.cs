using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Plant.Mom.Api.Entities;
using Plant.Mom.Api.Transfer;

namespace Plant.Mom.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class MomController : Controller
{
    private readonly PlantContext _context;

    public MomController(PlantContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IActionResult Get([FromQuery] int? id)
    {
        WebResult result;

        if (id == null)
            result = new(_context.Moms);
        else
            result = new(_context.Moms
                .Include(x => x.HumidityConfigurations)
                .ThenInclude(x => x.HumidityDaySchedules)
                .Include(x => x.HumidityConfigurations)
                .ThenInclude(x => x.HumidityTimeSchedules)
                .Include(x => x.LightingConfigurations)
                .ThenInclude(x => x.LightingDaySchedules)
                .Include(x => x.LightingConfigurations)
                .ThenInclude(x => x.LightingTimeSchedules)
                .FirstOrDefault(x => x.Id == id));

        return Ok(result);
    }

    [HttpPost]
    public IActionResult Post([FromBody] MomConfiguration mom)
    {
        WebResult result;

        result = new(_context.Moms.Add(mom).Entity);
       
        _context.SaveChanges();
        return Ok(result);
    }

    [HttpPatch]
    public IActionResult Patch([FromBody] MomConfiguration mom)
    {
        WebResult result;

        if (!_context.Moms.Any(x => x.Id == mom.Id)) return NotFound();

        result = new(_context.Moms.Update(mom).Entity);
        _context.SaveChanges();

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult Delete([FromQuery] int id)
    {
        WebResult result;
        MomConfiguration? mom = _context.Moms.Find(id);

        if (mom == null) return NotFound();

        result = new(_context.Moms.Remove(mom).Entity);
        _context.SaveChanges();

        return Ok(result);
    }
}
