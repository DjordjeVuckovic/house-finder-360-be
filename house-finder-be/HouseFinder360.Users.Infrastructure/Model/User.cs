﻿using FluentResults;
using HouseFinder360.Users.Infrastructure.Errors;
using Microsoft.AspNetCore.Identity;

namespace HouseFinder360.Users.Infrastructure.Model;

public class User : IdentityUser<Guid>
{
    public string FirstName { get; set; } = string.Empty;
    public string LastName { get; set; } = string.Empty;
    public List<UserRole> Roles { get;private set; } = new();
    public override string? Email { get; set; }

    public IEnumerable<string?> GetRoleNames() => Roles.Select(x => x.Name);
    public string?[] GetRoleNamesStringify() => Roles
        .Where(x => x.Name != null)
        .Select(x => x.Name)
        .ToArray();

    private readonly List<string> _allowedRoles = new() { "user","agency"};

    public Result AssignRole(string role)
    {
        if (!_allowedRoles.Contains(role.ToLower()))
        {
            return Result.Fail(UsersErrors.RoleAssign);
        }
        Roles.Add(new UserRole
        {
            Name = role
        });
        return Result.Ok();
    }
}