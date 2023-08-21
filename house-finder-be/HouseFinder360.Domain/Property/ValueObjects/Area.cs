﻿using FluentResults;
using HouseFinder360.BuildingBlocks.BuildingBlocks;
using HouseFinder360.Domain.Common.Errors;

namespace HouseFinder360.Domain.Property.ValueObjects;

public class Area : ValueObject
{
    public int SquadMeter { get; init; }
    public override IEnumerable<object> GetEqualityComponents()
    {
        yield return SquadMeter;
    }

    public static Result<Area> CreateArea(int squadMeter)
    {
        return squadMeter < 0 ? Result.Fail(DomainErrors.AreaErrors.NotValidArea) : new Area(squadMeter);
    }

    private Area(int squadMeter)
    {
        SquadMeter = squadMeter;
    }
    private Area()
    {
    }
}