using Microsoft.AspNetCore.Identity;

namespace Books.DAL.Entities.Identity
{
    public class AppUserLoginEntity : IdentityUserLogin<string>
    {
        public AppUserEntity? User { get; set; }
    }
}
