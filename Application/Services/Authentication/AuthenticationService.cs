using Application.DTOs;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Domain.Interfaces;

namespace Application.Services.Authentication;

public class AuthenticationService :  IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthenticationResponse Register(RegisterRequest registerRequest)
    {
        // 1. Validate if the user doesn't exist
        var userExist = _userRepository.GetByUsernameAsync(registerRequest.Username).Result;
        if (userExist != null)
        {
            throw new Exception("Username already exists");
        }
        
        // 2. Create user (generate unique ID)
        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
            Password = registerRequest.Password,
        };
        
        _userRepository.AddAsync(user);
        
        // 3. Create JWT Token
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(user, token);
    }
    
    public AuthenticationResponse Login(LoginRequest loginRequest)
    {
        
        var user = _userRepository.GetByUsernameAsync(loginRequest.Username).Result;
        
        if (user == null)
        {
            throw new Exception("Username doesn't exists");
        }

        if (user.Password != loginRequest.Password)
        {
            throw new Exception("Wrong password");
        }
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(user, token);
    }

}