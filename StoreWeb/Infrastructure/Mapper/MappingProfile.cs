using AutoMapper;
using Entities.Dtos;
using Entities.Models;
using Microsoft.AspNetCore.Identity;

namespace StoreWeb.Infrastructure.Mapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<ProductDtoForInsertion, Product>();
        CreateMap<CategoryDtoForInsertion, Category>();
        CreateMap<ProductDtoForUpdate, Product>().ReverseMap();
        CreateMap<CategoryDtoForUpdate, Category>().ReverseMap();
        CreateMap<UserDtoForCreation, IdentityUser>();
        CreateMap<UserDtoForUpdate, IdentityUser>().ReverseMap();
    }
}
