using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SimpleJournalApp.Domain.Entities;
using SimpleJournalApp.Application.Interface;

namespace SimpleJournalApp.WebAPI.Controllers;
[Authorize]
[ApiController]
[Route("api/[controller]")]
public class JournalEntryController : ControllerBase
{
    private readonly IJournalEntryService _service;

    public JournalEntryController(IJournalEntryService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get() => Ok(await _service.GetAllAsync());

    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id) => Ok(await _service.GetByIdAsync(id));

    [HttpPost]
    public async Task<IActionResult> Post([FromBody] List<JournalEntry> entries)
    {

        if (entries.Sum(x => x.DebitAmount) != entries.Sum(x => x.CreditAmount))
            return BadRequest("Debits and Credits must be equal");

        await _service.CreateAsync(entries);
            return Ok();
        
        

    }

    [HttpPut("{id}")]
    public async Task<IActionResult> Put(int id, [FromBody] JournalEntry entry)
    {
        if (id != entry.Id) return BadRequest();
        await _service.UpdateAsync(entry);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> Delete(int id)
    {
        await _service.DeleteAsync(id);
        return NoContent();
    }
}
