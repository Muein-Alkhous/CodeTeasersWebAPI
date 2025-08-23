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
            .Map(dest => dest.CategoryResponses, src => src.ProblemCategories
                .Select(pc => pc.Category)
                .Adapt<IEnumerable<CategoryResponse>>());
        
        TypeAdapterConfig<Category, CategoryResponse>
            .NewConfig();
    }
}