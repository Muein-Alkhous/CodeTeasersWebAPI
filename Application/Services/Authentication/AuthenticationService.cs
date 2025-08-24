using Application.DTOs;
using Application.DTOs.Request;
using Application.DTOs.Response;
using Application.Exceptions;
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
    private readonly IPasswordHasher<User> _passwordHasher;

    public AuthenticationService(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository, IPasswordHasher<User> passwordHasher)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
    }

    public async Task<AuthenticationResponse?> Register(RegisterRequest registerRequest)
    {
        // Check username or email conflict
        var existingByUsername = await _userRepository.GetByUsernameAsync(registerRequest.Username);
        var existingByEmail = await _userRepository.GetByEmailAsync(registerRequest.Email);
        if (existingByUsername is not null || existingByEmail is not null)
            throw new ConflictException($"User with the same username or email already exists.");

        var user = new User
        {
            Username = registerRequest.Username,
            Email = registerRequest.Email,
        };

        user.Password = HashPassword(user, registerRequest.Password);

        // Optionally initialize status/rank etc.
        user.UserStatus = new UserStatus
        {
            Rank = "Newbie"
        };

        await _userRepository.AddAsync(user);

        var userResponse = user.Adapt<UserResponse>();
        var token = _jwtTokenGenerator.Generate(user);

        return new AuthenticationResponse(userResponse, token.AccessToken, token.RefreshToken);
    }
    
    public async Task<AuthenticationResponse?> Login(LoginRequest loginRequest)
    {
        
        var user = await _userRepository.GetByUsernameAsync(loginRequest.Username);
        // Always return Unauthorized on bad credentials (don’t reveal which part failed)
        if (user is null || !VerifyPassword(user, loginRequest.Password, user.Password))
            throw new UnauthorizedAccessException("Invalid username or password.");

        var userResponse = user.Adapt<UserResponse>();
        var token = _jwtTokenGenerator.Generate(user);

        return new AuthenticationResponse(userResponse, token.AccessToken, token.RefreshToken);
    }

    
    
    // Helpers
    private string HashPassword(User user, string password) =>
        _passwordHasher.HashPassword(user, password);

    // providedPassword vs hashedPassword (order matters)
    private bool VerifyPassword(User user, string providedPassword, string hashedPassword) =>
        _passwordHasher.VerifyHashedPassword(user, hashedPassword, providedPassword)
        == PasswordVerificationResult.Success;

}