﻿using AutoMapper;
using Microsoft.EntityFrameworkCore;
using Project.DBOperations;
using Project.Mapping;

namespace Test.TestSetup;

public class CommonTestFixture
{
    public BookStoreDbContext Context { get; set; }

    public IMapper Mapper { get; set; }

    public CommonTestFixture()
    {
        var options = new DbContextOptionsBuilder<BookStoreDbContext>()
            .UseInMemoryDatabase(databaseName: "BookStoreTestDb").Options;

        Context = new BookStoreDbContext(options);
        Context.Database.EnsureCreated();
        Context.AddBooks();
        Context.AddGenres();
        Context.AddAuthors();
        Context.SaveChanges();

        Mapper = new MapperConfiguration(cfg => { cfg.AddProfile<MappingProfile>(); }).CreateMapper();
    }
}