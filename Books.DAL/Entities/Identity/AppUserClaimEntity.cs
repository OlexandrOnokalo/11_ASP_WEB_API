using Microsoft.AspNetCore.Identity;

namespace Books.DAL.Entities.Identity
{
    public class AppUserClaimEntity : IdentityUserClaim<string>
    {
        public AppUserEntity? User { get; set; } 
    }
}
