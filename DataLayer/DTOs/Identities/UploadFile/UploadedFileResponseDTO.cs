namespace KiaKooshar.Application.DTOs.Identities.UploadFile
{
    public class UploadedFileResponseDTO
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public Stream Data { get; set; }
    }
}
