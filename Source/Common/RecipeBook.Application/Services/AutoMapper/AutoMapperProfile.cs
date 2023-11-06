using AutoMapper;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Services.AutoMapper;

public class AutoMapperProfile : Profile
{
    public AutoMapperProfile()
    {
        CreateMap<SignUpRequest, Account>().ReverseMap();
    }
}