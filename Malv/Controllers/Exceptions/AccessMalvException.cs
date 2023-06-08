using Malv.Models;

namespace Malv.Controllers.Exceptions;

public class AccessMalvException : MalvException
{
    public AccessMalvException(Error_Res error) : base(error)
    {
    }
}