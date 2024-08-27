using Microsoft.AspNetCore.Mvc;
using MongoDB.Bson;
using App;
using MongoDB.Driver;
using AutoMapper;

[ApiController]
[Route("api")]
public class MyController : ControllerBase
    {
        private readonly IServiceDemo _myService;
    private readonly IMapper _mapper;


    public MyController(IServiceDemo myService,IMapper mapper)
        {
            _myService = myService;
        _mapper = mapper;   
        }

    [HttpGet]
    public async Task<ActionResult<List<User>>> Get()
    {
        var res = await _myService.GetAllAsync();
        return Ok(res);
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<User>> GetOneById(string id)
    {
        var entity = await _myService.GetByIdAsync(id);
        if (entity == null)
        {
            return NotFound();
        }
        return Ok(entity);
    }

    [HttpPost]
    public async Task<ActionResult> Post([FromBody] UserDto entity)
    {
        var dest=_mapper.Map<User>(entity);
        await _myService.InsertDocumentAsync(dest);
        return CreatedAtAction(nameof(Get), new { id = dest.Id }, entity);
    }

    [HttpPut]
    public async Task<ActionResult> Put([FromBody] User entity)
    {
        var existingEntity = await _myService.GetByIdAsync(entity.Id);
        if (existingEntity == null)
        {
            return NotFound();
        }
        await _myService.ReplaceDocumentAsync(entity);
        return NoContent();
    }

    [HttpDelete("{id}")]
    public async Task<ActionResult> Delete(string id)
    {
        var existingEntity = await _myService.GetByIdAsync(id);
        if (existingEntity == null)
        {
            return NotFound();
        }
        await _myService.DeleteDocumentAsync(id);
        return NoContent();
    }

}


