﻿using FluentResults;
using HouseFinder360.Application.Authentication.Common;
using HouseFinder360.Application.Common.Errors;
using HouseFinder360.Application.Common.Interfaces.Authentication;
using HouseFinder360.Application.Common.Interfaces.Persistence;
using MediatR;

namespace HouseFinder360.Application.Authentication.Commands.Register;

public class RegisterCommandHandler: IRequestHandler<RegisterCommand, Result<AuthResult>>
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public RegisterCommandHandler(IJwtTokenGenerator jwtTokenGenerator, IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public async Task<Result<AuthResult>> Handle(RegisterCommand request, CancellationToken cancellationToken)
    {
        var user = _userRepository.GetUserByEmail(request.Email);
        if (user is not null)
        {
            var error = await Task.FromResult(
                Result.Fail<AuthResult>(ApplicationErrors.Generic.BadRequest));
            return error;
        }
        var userId = Guid.NewGuid();
        var token = _jwtTokenGenerator.GenerateToken(userId, request.FirstName, request.LastName,request.Email);
        return new AuthResult(token);
    }
}