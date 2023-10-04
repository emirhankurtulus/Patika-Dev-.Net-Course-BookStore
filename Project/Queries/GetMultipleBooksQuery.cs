﻿namespace Project.Queries;

public class GetMultipleBooksQuery
{
    public Guid? AuthorId { get; set; }

    public string? Title { get; set; }

    public string? Genre { get; set; }

    public int? PageCount { get; set; }

    public DateOnly? PublishDate { get; set; }

    public bool? Active { get; set; }
}
