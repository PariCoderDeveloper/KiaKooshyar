namespace KiaKooshar.Application.Features.Interfaces.Files
{
    public interface IFileConverter
    {
        Task<byte[]> ToByteAsync (
            Stream stream,
            CancellationToken cancellationToken = default
            );
        public Stream ToStream (
            byte[] data,
            CancellationToken cancellationToken = default
            );
    }
}
