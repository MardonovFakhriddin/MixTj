using System.ComponentModel.DataAnnotations;

namespace Domain.Entites;

public class Comment : BaseEntity
{
    [Required, MinLength(20), MaxLength(5000)]
    public string Text { get; set; }

    public int? ParentCommentId { get; set; }

    public Comment ParentComment { get; set; }

    public List<Comment> Replies { get; set; } = new();
}