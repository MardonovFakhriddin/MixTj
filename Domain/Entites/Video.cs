using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Video : BaseEntity
{
    [Required, MinLength(5)]
    public string Title { get; set; }

    [MaxLength(200)]
    public string Description { get; set; }

    [Required, Url]
    public string URL { get; set; }

    [Required]
    public string VideoType { get; set; }
}