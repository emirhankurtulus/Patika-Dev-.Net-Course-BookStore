﻿using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Project.Commands;
using Project.Commands.Handlers;
using Project.Commands.Validators;
using Project.DBOperations;
using Project.DTO;
using Project.Queries.Handlers;
using Project.Services;

namespace Project.Controllers;

[ApiController]
[Route("[controller]s")]
public class UserController : Controller
{
    private readonly IDbContext _context;
    private readonly IMapper _mapper;
    private readonly IPasswordHelper _passwordHelper;
    private readonly IAuthenticationService _authenticationService;

    public UserController(
        IDbContext context,
        IMapper mapper,
        IPasswordHelper
        passwordHelper,
        IAuthenticationService authenticationService)
    {
        _context = context;
        _mapper = mapper;
        _passwordHelper = passwordHelper;
        _authenticationService = authenticationService;
    }

    [Authorize]
    [HttpGet("me")]
    public IActionResult GetUserById()
    {
        var token = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);

        var query = new GetUserHandler(_context, _mapper, _authenticationService);

        var result = query.Handle(token);

        return Ok(result);
    }

    [HttpPost("register")]
    public IActionResult AddUser([FromBody] SaveUserCommand entity)
    {
        var validator = new SaveUserValidator();

        validator.ValidateAndThrow(entity);

        var command = new SaveUserCommandHandler(_context, _mapper, _passwordHelper, _authenticationService);

        command.Handle(entity, null);

        return Ok();
    }

    [HttpPost("login")]
    public ActionResult<TokenDto> CreateToken([FromBody] CreateTokenCommand login)
    {
        var command = new CreateTokenCommandHandler(_context, _passwordHelper, _authenticationService);

        var token = command.Handle(login);

        return token;
    }

    [HttpPut]
    public IActionResult UpdatedAuthor([FromBody] SaveUserCommand entity)
    {
        if (entity.Id is null)
        {
            return BadRequest("Id is null");
        }

        var validator = new SaveUserValidator();

        validator.ValidateAndThrow(entity);

        var token = HttpContext.Request.Headers["Authorization"].ToString()?.Replace("Bearer ", string.Empty);

        var command = new SaveUserCommandHandler(_context, _mapper, _passwordHelper, _authenticationService);

        command.Handle(entity, token);

        return Ok();
    }
}