namespace Domain.Dtos;

public class TagCreateDto
{
    public string Name { get; set; }
}

public class TagUpdateDto
{
    public int Id { get; set; }
    public string Name { get; set; }
}

public class TagGetDto
{
    public string Name { get; set; }
}