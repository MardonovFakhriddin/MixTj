namespace Domain.Dtos;

public class UploadedFileCreateDto
{
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}

public class UploadedFileGetDto
{
    public int Id { get; set; }
    public string FileName { get; set; }
    public string FilePath { get; set; }
    public long FileSize { get; set; }
}