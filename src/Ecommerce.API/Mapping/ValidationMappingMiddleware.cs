namespace Ecommerce.Mapping;

using Contracts.Responses;
using FluentValidation;

public class ValidationMappingMiddleware
{
    private readonly RequestDelegate _next;

    public ValidationMappingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task InvokeAsync(HttpContext httpContext)
    {
        try
        {
            await _next(httpContext);
        }
        catch (ValidationException validationException)
        {
            httpContext.Response.StatusCode = 400;
            var validationFailureResponse = new ValidationFailureResponse()
            {
                Errors = validationException.Errors.Select(x => new ValidationResponse()
                {
                    PropertyName = x.PropertyName, Message = x.ErrorMessage
                })
            };
            await httpContext.Response.WriteAsJsonAsync(validationFailureResponse);
        }
    }
}
