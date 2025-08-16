using Application.DTOs;
using Domain.Entities;
using Mapster;

namespace Application.Profiles;

public class UserProfile : IRegister
{
    public void Register(TypeAdapterConfig config)
    {
        config.NewConfig<User, UserResponse>()
            .Map(dest => dest.UserStatus, src => src.UserStatus.Adapt<UserStatusDto>());
    }
}