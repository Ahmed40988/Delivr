using System.Security.Claims;

namespace Deliver.Api.Abstractions
{
    public static class UserExtensions
    {
        public static int GetUserId(this ClaimsPrincipal user)
        {
            var idValue = user.FindFirstValue(ClaimTypes.NameIdentifier);

            return int.TryParse(idValue, out var id)
                ? id
                : throw new InvalidOperationException("User ID claim is missing or not an integer.");
        }
    }

}
