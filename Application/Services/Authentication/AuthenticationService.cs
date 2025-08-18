using Application.DTOs;
using Application.Interfaces.Authentication;
using Domain.Entities;
using Domain.Interfaces;
using Mapster;
using MapsterMapper;
using Microsoft.AspNetCore.Identity;

namespace Application.Services.Authentication;

public class AuthenticationService :  IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;
    private readonly IMapper _mapper;
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IMapper mapper, IPasswordHasher<User> passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _mapper = mapper;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        // 1. Validate if the user doesn't exist
        var userExist = await _userRepository.GetByUsernameAsync(registerRequest.Username);
        if (userExist != null)
        {
            return null;
        }
        
        // Hashing the password
        
        // 2. Create user (generate unique ID)
        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
        };
        
        // Hashing password
        var hashedPassword = HashPassword(user, registerRequest.Password);
        user.Password = hashedPassword;

        // Adding status to user
        var userStatus = new UserStatus();
        user.UserStatus = userStatus;
        
        await _userRepository.AddAsync(user);

        var userResponse = _mapper.Map<UserResponse>(user);
        
        // 3. Create JWT Token
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(userResponse, token);
    }
    
    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        
        if (user == null)
        {
            return null; // User doesn't exist
        }

        var result = VerifyPassword(user, user.Password, loginRequest.Password);
        if (result)
        {
            return null;
        }
        
        var userResponse = _mapper.Map<UserResponse>(user);
        
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthenticationResponse(userResponse, token);
    }

    
    
    // Helper Functions
    private string HashPassword(User user, string password)
    {
        return _passwordHasher.HashPassword(user, password);
    }

    private bool VerifyPassword(User user, string password, string hashedPassword)
    {
        var result = _passwordHasher.VerifyHashedPassword(user, hashedPassword, password);
        return result == PasswordVerificationResult.Success;
    }

}