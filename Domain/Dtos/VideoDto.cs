namespace Domain.Dtos;

public class VideoCreateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public string VideoType { get; set; }
}

public class VideoUpdateDto
{
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public string VideoType { get; set; }
}

public class VideoGetDto
{
    public int Id { get; set; }
    public string Title { get; set; }
    public string Description { get; set; }
    public string URL { get; set; }
    public string VideoType { get; set; }
}