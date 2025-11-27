using Microsoft.AspNetCore.Identity;

namespace EHS_Benjamin_Pasic.Models
{
    public class AppUser : IdentityUser
    {
        public ICollection<UserSavedNews> SavedNews { get; set; }
    }
}
