namespace Domain.Dtos;

public class NewsCreateDto
{
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; }
}

public class NewsUpdateDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; }
}

public class NewsGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Content { get; set; }
    public string Category { get; set; }
    public List<string> Tags { get; set; }
}