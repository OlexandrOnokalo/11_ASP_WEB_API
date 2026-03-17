using Microsoft.AspNetCore.Identity;

namespace Books.DAL.Entities.Identity
{
    public class AppUserRoleEntity : IdentityUserRole<string>
    {
        public AppUserEntity? User { get; set; }
        public AppRoleEntity? Role { get; set; }
    }
}
