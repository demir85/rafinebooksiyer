using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace Eticaret12.Models
{
    public class IdentityUserRoley : IdentityUserRole<string>
    {
        [ScaffoldColumn(false)]
        public string Id { get; set; }
    }
}
