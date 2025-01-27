using FluentResults;
using FluentValidation;
using HouseFinder360.Domain.BuildingBlocks.Errors;
using MediatR;
using Microsoft.Extensions.Logging;

namespace HouseFinder360.RealEstates.Application.Common.Behaviours;

public class ValidationPipelineBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
    where TRequest : IRequest<TResponse>
    where TResponse : ResultBase, new()
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationPipelineBehaviour(
        IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
    {
        if (!_validators.Any()) return await next();
        var context = new ValidationContext<TRequest>(request);

        var validationResults = await Task.WhenAll(
            _validators.Select(v =>
                v.ValidateAsync(context, cancellationToken)));

        var failures = validationResults
            .Where(r => r.Errors.Any())
            .SelectMany(r => r.Errors.Select(x => new ErrorResult(x.ErrorMessage, ErrorStatusCodes.BadRequest)))
            .ToList();

        if (!failures.Any()) return await next();
        return Result.Fail(failures) as TResponse ?? new TResponse();
    }
}
