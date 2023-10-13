using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities;

public class Author : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string FirstName { get; set; }

    public string Surname { get; set; }

    public DateOnly BirthDate { get; set; }
}