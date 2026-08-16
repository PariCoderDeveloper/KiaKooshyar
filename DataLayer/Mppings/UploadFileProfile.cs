using AutoMapper;
using KiaKooshar.Application.DTOs.Identities.UploadFile;
using KiaKooshar.Application.Features.Identities.UploadFile.Requests.Command;
using KiaKooshar.Domain.Entities.UploadFile;

namespace KiaKooshar.Application.Mppings
{
    public class UploadFileProfile : Profile
    {
        public UploadFileProfile ()
        {
            CreateMap<UploadedFileCommand, UploadedFile> ().ReverseMap ();
            CreateMap<UploadedFile, UploadedFileResponseDTO> ()
                .ForMember (
                    dest => dest.Data,
                    opt => opt.Ignore ()
                );
        }
    }
}
