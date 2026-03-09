using Application.Common;
using FluentValidation;
using MediatR;

public class ValidationBehavior<TRequest, TResponse>
    : IPipelineBehavior<TRequest, TResponse>
    where TResponse : class
{
    private readonly IEnumerable<IValidator<TRequest>> _validators;

    public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
    {
        _validators = validators;
    }

    public async Task<TResponse> Handle(
        TRequest request,
        RequestHandlerDelegate<TResponse> next,
        CancellationToken cancellationToken)
    {
        if (_validators.Any())
        {
            var context = new ValidationContext<TRequest>(request);

            var failures = _validators
                .Select(v => v.Validate(context))
                .SelectMany(r => r.Errors)
                .Where(f => f != null)
                .ToList();

            if (failures.Any())
            {
                var errors = failures.Select(e => e.ErrorMessage).ToList();

                var responseType = typeof(TResponse);

                var response = Activator.CreateInstance(responseType);

                var successProp = responseType.GetProperty("Success");
                var messageProp = responseType.GetProperty("Message");
                var errorsProp = responseType.GetProperty("Errors");

                successProp?.SetValue(response, false);
                messageProp?.SetValue(response, "Validation failed");
                errorsProp?.SetValue(response, errors);

                return (TResponse)response!;
            }
        }

        return await next();
    }
}