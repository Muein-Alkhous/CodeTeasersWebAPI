using Application.DTOs;
using AutoMapper;
using Domain.Entities;

namespace Application.Profiles;

public class ProblemProfile : Profile
{
    public ProblemProfile()
    {
        CreateMap<Problem, ProblemResponse>();
        CreateMap<ProblemRequest, Problem>();
    }
    
}