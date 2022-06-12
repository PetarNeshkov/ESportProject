using System.Security.Claims;

using static E_SportManager.Common.GlobalConstants.Administrator;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class ClaimsPrincipalExtensions
    {
        public static bool IsAdministrator(this ClaimsPrincipal claimsPrincipal)
          => claimsPrincipal.IsInRole(AdministratorRoleName);

        public static string Id(this ClaimsPrincipal user)
           => user.FindFirst(ClaimTypes.NameIdentifier).Value;
    }
}
