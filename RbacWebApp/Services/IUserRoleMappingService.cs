using System.Security.Claims;
using System.Threading.Tasks;

namespace RbacWebApp.Services;

public interface IUserRoleMappingService
{
    Task MapRolesAsync(ClaimsPrincipal user);
}
