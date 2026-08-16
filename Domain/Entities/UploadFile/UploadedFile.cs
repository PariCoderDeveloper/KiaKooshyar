using KiaKooshar.Domain.Entities.BaseEntities;

namespace KiaKooshar.Domain.Entities.UploadFile
{
    public class UploadedFile : BaseEntity
    {
        public string FileName { get; set; } = null!;
        public string ContentType { get; set; } = null!;
        public long FileSize { get; set; }
        public byte[] Data { get; set; } = [];
    }
}
