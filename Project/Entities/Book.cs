using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities;

public class Book : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public Guid GenreId { get; set; }

    public string Title { get; set; }

    public int PageCount { get; set; }

    public DateOnly PublishDate { get; set; }
}