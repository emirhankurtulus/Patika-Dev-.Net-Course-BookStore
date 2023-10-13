using System.ComponentModel.DataAnnotations.Schema;

namespace Project.Entities;

public class Genre : BaseEntity
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public Guid Id { get; set; }

    public string Name { get; set; }
}