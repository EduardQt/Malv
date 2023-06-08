using System.Security.Claims;

namespace Malv.Controllers.Helpers;

public static class UserHelpers
{
    public static int GetUserId(this ClaimsPrincipal claimsPrincipal)
    {
        return int.Parse(claimsPrincipal.Identity.Name);
    }
}