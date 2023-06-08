using Malv.Models;

namespace Malv.Controllers.Exceptions;

public class BusinessMalvException : MalvException
{
    public BusinessMalvException(Error_Res error) : base(error)
    {
    }
}