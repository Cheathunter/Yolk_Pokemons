
namespace Yolk_Pokemon.Application.Responses
{
    public class GenericResponse<T>
    {
        public required bool Success { get; init; }
        public required int StatusCode { get; init; }
        public required string Message { get; init; }
        public T[]? Data { get; init; }
    }
}