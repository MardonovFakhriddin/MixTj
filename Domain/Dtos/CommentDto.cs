namespace Domain.Dtos;

public class CommentCreateDto
{
    public string Text { get; set; }
    public int? ParentCommentId { get; set; }
}

public class CommentUpdateDto
{
    public int Id { get; set; }
    public string Text { get; set; }
}

public class CommentGetDto
{
    public int Id { get; set; }
    public string Text { get; set; }
    public int? ParentCommentId { get; set; }
}