using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class News : BaseEntity
{
    [Required, MinLength(10)]
    public string Title { get; set; }

    [Required, MinLength(100)]
    public string Content { get; set; }

    [Required]
    public string Category { get; set; }

    public List<Tag> Tags { get; set; } = new();
}