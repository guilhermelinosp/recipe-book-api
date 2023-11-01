using AutoMapper;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Services.AutoMapper;

public class AutoMapperService : Profile
{
    public AutoMapperService()
    {
        CreateMap<SignUpRequest, User>().ReverseMap();
    }
}