using Microsoft.AspNetCore.Identity;

namespace Eticaret12.Models
{
    public class UserRol
    {
        public int Id { get; set; }
        public string UserId { get; set; }
       // public IdentityUser User { get; set; }
        public int RolId { get; set; }
       //public IdentityRole Rol { get; set; }
    }
}
