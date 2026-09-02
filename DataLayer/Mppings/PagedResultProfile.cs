using AutoMapper;
using KiaKooshar.Application.Common.Models;

namespace KiaKooshar.Application.Mppings
{
    public class PagedResultProfile : Profile
    {
        public PagedResultProfile ()
        {
            CreateMap (typeof (PagedResult<>), typeof (PagedResult<>));
        }
    }
}
