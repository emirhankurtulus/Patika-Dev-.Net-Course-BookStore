﻿using AutoMapper;
using Project.DBOperations;
using Project.DTO;
using Project.Entities;
using Project.Enums;

namespace Project.Queries.Handlers;

public class GetBookHandler
{
    private readonly BookStoreDbContext _dbContext;
    private readonly IMapper _mapper;

    public GetBookHandler(BookStoreDbContext dbContext, IMapper mapper)
    {
        _dbContext = dbContext;
        _mapper = mapper;
    }

    public BookDto Handle(Guid id)
    {
        var book = _dbContext.Books.FirstOrDefault(x => x.Id == id);

        var result = _mapper.Map<BookDto>(book);

        return result;
    }
}