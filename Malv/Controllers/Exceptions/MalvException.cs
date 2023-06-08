using Malv.Models;

namespace Malv.Controllers.Exceptions;

public abstract class MalvException : Exception
{
    public Error_Res Error { get; }

    public MalvException(Error_Res error)
    {
        Error = error;
    }
}