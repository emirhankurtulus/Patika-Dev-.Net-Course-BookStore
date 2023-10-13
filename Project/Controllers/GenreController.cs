using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Project.Commands;
using Project.Commands.Handlers;
using Project.Commands.Validators;
using Project.DBOperations;
using Project.Queries;
using Project.Queries.Handlers;

namespace Project.Controllers;

[ApiController]
[Route("[controller]s")]
public class GenreController : Controller
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public GenreController(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetGenreById(Guid id)
    {
        var query = new GetGenreHandler(_context, _mapper);

        var result = query.Handle(id);

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetGenreByFilters([FromQuery] GetMultipleGenresQuery queries)
    {
        var query = new GetMultipleGenresHandler(_context, _mapper);

        var result = query.Handle(queries);

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult DeleteGenre(Guid? id)
    {
        if (id is null)
        {
            return BadRequest("Id is null");
        }

        var command = new DeleteGenreCommandHandler(_context);

        command.Handle(id.Value);

        return Ok();
    }

    [HttpPut]
    public IActionResult UpdatedGenre([FromBody] SaveGenreCommand entity)
    {
        if (entity.Id is null)
        {
            return BadRequest("Id is null");
        }

        var command = new SaveGenreCommandHandler(_context, _mapper);

        var validator = new SaveGenreValidator();

        validator.ValidateAndThrow(entity);

        command.Handle(entity);

        return Ok();
    }

    [HttpPost]
    public IActionResult AddGenre([FromBody] SaveGenreCommand entity)
    {
        var command = new SaveGenreCommandHandler(_context, _mapper);

        var validator = new SaveGenreValidator();

        validator.ValidateAndThrow(entity);

        command.Handle(entity);

        return Ok();
    }
}