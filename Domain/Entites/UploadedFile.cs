using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class UploadedFile : BaseEntity
{
    [Required]
    public string FileName { get; set; }

    [Required]
    [RegularExpression(@".*\.(jpg|png|pdf|mp4|mov|avi)$", ErrorMessage = "Invalid file format.")]
    public string FilePath { get; set; }

    [Range(0, 104857600)]
    public long FileSize { get; set; }
}