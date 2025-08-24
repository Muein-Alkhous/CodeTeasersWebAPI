using Application.DTOs;
using Application.DTOs.Response;
using Domain.Entities;
using Mapster;

namespace Application;

public static class MappingConfig
{
    public static void Configure()
    {
        TypeAdapterConfig<Problem, ProblemResponse>
            .NewConfig()
            .Map(dest => dest.CategoryResponses,
                src => src.ProblemCategories
                .Select(pc => pc.Category)
                .Adapt<IEnumerable<CategoryResponse>>())
            .Map(dest => dest.Description,
                src => src.Description.Adapt<DescriptionResponse>());
        
        TypeAdapterConfig<Category, CategoryResponse>
            .NewConfig();

        TypeAdapterConfig<Description, DescriptionResponse>
            .NewConfig();
        
        TypeAdapterConfig<User, UserResponse>
            .NewConfig();
    }
}