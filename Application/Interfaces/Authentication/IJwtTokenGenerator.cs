using Domain.Entities;
using Infrastructure.Authentication;

namespace Application.Interfaces.Authentication;

public interface IJwtTokenGenerator
{
    JwtResult Generate(User user);
}