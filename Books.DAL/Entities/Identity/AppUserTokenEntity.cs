using Microsoft.AspNetCore.Identity;

namespace Books.DAL.Entities.Identity
{
    public class AppUserTokenEntity : IdentityUserToken<string>
    {
        public AppUserEntity? User { get; set; }
    }
}
