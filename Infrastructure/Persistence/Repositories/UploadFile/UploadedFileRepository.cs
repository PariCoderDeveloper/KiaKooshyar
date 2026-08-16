using KiaKooshar.Application.Features.Interfaces.Repositories;
using KiaKooshar.Domain.Entities.UploadFile;
using KiaKooshar.Infrastructure.Persistence.Repositories.Generic;

namespace KiaKooshar.Infrastructure.Persistence.Repositories.UploadFile
{
    public class UploadedFileRepository :
        GenericRepository<UploadedFile>,
        IUploadedFileRepository
    {
        public UploadedFileRepository (
            DatabaseContext context
            ) : base (context)
        {
        }
    }
}
