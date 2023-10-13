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
public class AuthorController : Controller
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;

    public AuthorController(IDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetAuthoryId(Guid id)
    {
        var query = new GetAuthorHandler(_context, _mapper);

        var result = query.Handle(id);

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetAuthorByFilters([FromQuery] GetMultipleAuthorsQuery queries)
    {
        var query = new GetMultipleAuthorsHandler(_context, _mapper);

        var result = query.Handle(queries);

        return Ok(result);
    }

    [HttpDelete]
    public IActionResult DeleteAuthor(Guid? id)
    {
        if (id is null)
        {
            return BadRequest("Id is null");
        }

        var command = new DeleteAuthorCommandHandler(_context);

        command.Handle(id.Value);

        return Ok();
    }

    [HttpPost]
    public IActionResult AddAuthor([FromBody] SaveAuthorCommand entity)
    {
        var command = new SaveAuthorCommandHandler(_context, _mapper);

        var validator = new SaveAuthorValidator();

        validator.ValidateAndThrow(entity);

        command.Handle(entity);

        return Ok();
    }

    [HttpPut]
    public IActionResult UpdatedAuthor([FromBody] SaveAuthorCommand entity)
    {
        if (entity.Id is null)
        {
            return BadRequest("Id is null");
        }

        var command = new SaveAuthorCommandHandler(_context, _mapper);

        var validator = new SaveAuthorValidator();

        validator.ValidateAndThrow(entity);

        command.Handle(entity);

        return Ok();
    }
}