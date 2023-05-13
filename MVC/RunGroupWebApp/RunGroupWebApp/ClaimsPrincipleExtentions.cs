using System.Security.Claims;

namespace RunGroupWebApp
{
    public static class ClaimsPrincipleExtentions
    {
        public static string GetUserId(this ClaimsPrincipal user)
        {
            return user.FindFirstValue(ClaimTypes.NameIdentifier) as string;
        }
    }
}
