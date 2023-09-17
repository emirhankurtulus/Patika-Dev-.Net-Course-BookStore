using System.ComponentModel.DataAnnotations.Schema;

namespace Project;

public class Book
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public int GenreId { get; set; }

    public string Title { get; set; }

    public int PageCount { get; set; }

    public DateOnly? PublishDate { get; set; }
}