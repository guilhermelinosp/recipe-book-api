using AutoMapper;
using RecipeBook.Domain.Dtos.Requests;
using RecipeBook.Domain.Entities;

namespace RecipeBook.Application.Services.AutoMapper;

public class AutoMapperController : Profile
{
    public AutoMapperController()
    {
        CreateMap<SignUpRequest, User>().ReverseMap();
    }
}