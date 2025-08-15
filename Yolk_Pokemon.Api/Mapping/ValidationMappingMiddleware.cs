
using FluentValidation;
using System.Text.RegularExpressions;
using Yolk_Pokemon.Application.Responses;

namespace Yolk_Pokemon.Api.Mapping
{
    public partial class ValidationMappingMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        private const string RequiredFieldNotFilledMessage = "Required field in the request was not filled.";
        private const string FieldFilledInccorectlyMessage = "At least one of the fields not met requirements.";

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ValidationException ex)
            {
                context.Response.StatusCode = 400;
                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = ex.Errors.Select(x => new ValidationResponse
                    {
                        Message = x.ErrorMessage,
                        PropertyName = x.PropertyName
                    })
                };

                await context.Response.WriteAsJsonAsync(validationFailureResponse
                    .MapToGenericResponse(FieldFilledInccorectlyMessage, StatusCodes.Status400BadRequest, false));
            }
            catch (BadHttpRequestException ex)
            {
                context.Response.StatusCode = StatusCodes.Status400BadRequest;

                string? propertyName = ex.InnerException == null ? null : GetMissingRequiredProperties(ex.InnerException!.Message);
                string message = ex.InnerException == null ? ex.Message : string.Concat(ex.Message, " ", ex.InnerException.Message);

                var validationFailureResponse = new ValidationFailureResponse
                {
                    Errors = [
                        new ValidationResponse
                        {
                            Message = message,
                            PropertyName = propertyName
                        }
                    ]
                };
                
                await context.Response.WriteAsJsonAsync(validationFailureResponse
                    .MapToGenericResponse(RequiredFieldNotFilledMessage, StatusCodes.Status400BadRequest, false));
            }
        }

        private static string? GetMissingRequiredProperties(string message)
        {
            if (message.Contains("required properties"))
            {
                var regex = PropertyNameRegex();
                var matches = regex.Matches(message);

                return matches.LastOrDefault()?.Groups[1].Value;
            }

            return null;
        }

        [GeneratedRegex("'([^']+)'")]
        private static partial Regex PropertyNameRegex();
    }
}