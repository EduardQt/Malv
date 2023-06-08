using Malv.Controllers.Exceptions;

namespace Malv.Filters;

public static class FilterHelpers
{
    public static int ExceptionToStatusCode(this MalvException exception)
    {
        if (exception is BusinessMalvException) return StatusCodes.Status400BadRequest;
        if (exception is AccessMalvException) return StatusCodes.Status401Unauthorized;

        throw new InvalidCastException($"Exception of type: {nameof(exception)} is not known");
    }
}