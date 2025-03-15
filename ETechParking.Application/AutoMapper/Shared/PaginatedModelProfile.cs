using AutoMapper;
using ETechParking.Application.Dtos.Shared.Paginations;
using ETechParking.Domain.Models.Shared;

namespace ETechParking.Application.AutoMapper.Shared;

public class PaginatedModelProfile : Profile
{
    public PaginatedModelProfile()
    {
        CreateMap<PaginatedModelDto, PaginatedModel>();
    }
}
