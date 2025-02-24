using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Tag : BaseEntity
{
    [Required, MinLength(5)]
    public string Name { get; set; }
}