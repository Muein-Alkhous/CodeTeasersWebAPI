using Application.DTOs;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using MapsterMapper;

namespace Application.Services.Authentication;

public class AuthenticationService :  IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;


    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMapper mapper)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
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

        var userStatus = new UserStatus();
        
        user.UserStatus = userStatus;
        
        _userRepository.AddAsync(user);

        var userResponse = _mapper.Map<UserResponse>(user);
        
        // 3. Create JWT Token
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(userResponse, token);
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
        
        var userResponse = _mapper.Map<UserResponse>(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(userResponse, token);
    }

}