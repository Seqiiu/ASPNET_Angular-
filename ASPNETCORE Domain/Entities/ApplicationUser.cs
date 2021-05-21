using Microsoft.AspNetCore.Identity;

namespace ASPNET_CORE_Domain
{
    public class ApplicationUser : IdentityUser
    {
        public string FirstName { get; set; }
        
        public string LastName { get; set; }
        
        public string Adres { get; set; }

    }
}
