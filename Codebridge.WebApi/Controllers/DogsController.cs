using Codebridge.Models;
using Codebridge.Service;
using Microsoft.AspNetCore.Mvc;
using AspNetCoreRateLimit;
using Codebridge.Data;

namespace Codebridge.Controllers;

[ApiController]

[Route("[controller]")]
public class DogsController : ControllerBase
{
    private readonly ILogger<DogsController> _logger;

    private readonly IDogService _service;

    // GET
    public DogsController(IDogService service, ILogger<DogsController> logger)
    {
        _service = service;
        _logger = logger;
    }
    
    
    [HttpGet]
    public async Task<IEnumerable<Dog>> GetAll()
    {
        var dogsList = await _service.GetDogsList();
        
        return dogsList;
    }


    //[ApiExplorerSettings(GroupName = "Dogs")]
    [HttpGet("sorted")]
    public async Task<IActionResult> GetSortedList([FromQuery] string attribute, [FromQuery] string order,[FromQuery] int pageNumber, 
        [FromQuery]   int pageSize)
    {
        try
        {
            var dogsList = await _service.SortedDogList(attribute, order,pageNumber,pageSize);
            if (dogsList is null)
            {
                _logger.LogInformation("Elements not found");
                return NotFound();
            }

            return Ok(dogsList);
        }
        catch (Exception e)
        {
            Console.WriteLine(e.Message);
            throw;
        }
    }

    [HttpPost]
    public async Task<ActionResult<DogDto>> CreateDog(DogDto dogDto) 
    {
        var createdDog = await _service.CreateDogAsync(dogDto);
        return Ok(createdDog);
    }
}