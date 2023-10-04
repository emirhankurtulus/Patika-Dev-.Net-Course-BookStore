using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Project.Commands;
using Project.Commands.Books;
using Project.Commands.Handlers;
using Project.Commands.Validators;
using Project.DBOperations;
using Project.Queries;
using Project.Queries.Handlers;

namespace Project.Controllers;

[ApiController]
[Route("[controller]s")]
public class BookController : Controller
{
    private readonly BookStoreDbContext _context;
    private readonly IMapper _mapper;

    public BookController(BookStoreDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    [HttpGet("{id}")]
    public IActionResult GetBookByID(Guid id)
    {
        var query = new GetBookHandler(_context, _mapper);

        var result = query.Handle(id);

        return Ok(result);
    }

    [HttpGet]
    public IActionResult GetBookByFilters([FromQuery] GetMultipleBooksQuery queries)
    {
        var query = new GetMultipleBooksHandler(_context, _mapper);

        var result = query.Handle(queries);

        return Ok(result);
    }

    [HttpPost]
    public IActionResult AddBook([FromBody] SaveBookCommand entity)
    {
            var command = new SaveBookCommandHandler(_context, _mapper);

            var validator = new SaveBookValidator();

            validator.ValidateAndThrow(entity);

            command.Handle(entity);

            return Ok();
    }

    [HttpDelete("{id}")]
    public IActionResult DeleteBook(Guid? id)
    {
        if (id is null)
        {
            return BadRequest("Id is null");
        }

        var command = new DeleteBookCommandHandler(_context);

        command.Handle(id.Value);

        return Ok();
    }

    [HttpPut]
    public IActionResult UpdatedBook([FromBody] SaveBookCommand entity)
    {
        if (entity.Id is null)
        {
            return BadRequest("Id is null");
        }

            var command = new SaveBookCommandHandler(_context, _mapper);

            var validator = new SaveBookValidator();
           
            validator.ValidateAndThrow(entity);

            command.Handle(entity);
            
            return Ok();
    }
}
