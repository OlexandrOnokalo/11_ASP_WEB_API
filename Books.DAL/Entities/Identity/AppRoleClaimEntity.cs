using Microsoft.AspNetCore.Identity;

namespace Books.DAL.Entities.Identity
{
    public class AppRoleClaimEntity : IdentityRoleClaim<string>
    {
        public AppRoleEntity? Role { get; set; }
    }
}
