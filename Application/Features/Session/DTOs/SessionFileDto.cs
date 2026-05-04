namespace Application.Features.Session.DTOs
{
    public class SessionFileDto
    {
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }
        public string Content { get; set; }
        public int SessionId { get; set; }
    }
}
