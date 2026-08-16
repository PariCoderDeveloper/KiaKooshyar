using KiaKooshar.Application.Features.Interfaces.Files;

namespace KiaKooshar.Infrastructure.Files
{
    public sealed class FileConverter
        : IFileConverter
    {
        public async Task<byte[]> ToByteAsync (
            Stream stream,
            CancellationToken cancellationToken = default
            )
        {
            using var memoryStream = new MemoryStream ();
            await stream.CopyToAsync (
                memoryStream,
                cancellationToken
                );
            return memoryStream.ToArray ();
        }
        public Stream ToStream (
            byte[] data,
            CancellationToken cancellationToken = default
            )
        {
            cancellationToken.ThrowIfCancellationRequested ();
            return new MemoryStream (data);
        }
    }
}
