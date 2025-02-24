namespace Domain.Dtos;

public class UserCreateDto
{
    public string Email { get; set; }
    public string Password { get; set; }
    public string Nickname { get; set; }
    public string Profile { get; set; }
}

public class UserUpdateDto
{
    public string Id { get; set; }
    public string Nickname { get; set; }
    public string Profile { get; set; }
}

public class UserGetDto
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Nickname { get; set; }
    public string Profile { get; set; }
}