﻿using HouseFinder360.Application.Common.Dtos.Shared;
using HouseFinder360.Application.Common.Interfaces.Persistence.Generic;
using HouseFinder360.Application.Common.Pagination;
using HouseFinder360.Application.Property.Dto;
using HouseFinder360.Application.Property.Mapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace HouseFinder360.Application.Property.Queries.GetPropertiesPaginite;

public class GetPropertiesPaginiteQueryHandler : IRequestHandler<GetPropertiesPaginiteQuery,PagedResponse<PropertyResponse>>
{
    private readonly IDbContext _dbContext;

    public GetPropertiesPaginiteQueryHandler(IDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<PagedResponse<PropertyResponse>> Handle(GetPropertiesPaginiteQuery request, CancellationToken cancellationToken)
    {
        var query = _dbContext.Properties.AsQueryable();
        request.Pagination.TotalItems = await query.CountAsync(cancellationToken);

        var propertyList = await query
            .OrderByDescending(x => x.Price.Value)
            .Skip((request.Pagination.CurrentPage - 1) * request.Pagination.PageSize)
            .Take(request.Pagination.PageSize)
            .Include(x => x.Address)
            .Include(x => x.Photos)
            .ToListAsync(cancellationToken);
        var mapped = propertyList.Select(PropertyMapper.MapProperty);
        return new PagedResponse<PropertyResponse>
        {
            Data = mapped,
            Pagination = request.Pagination
        };
    }
}