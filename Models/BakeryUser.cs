using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace SW4DAAssignment3.Models
{
    public class BakeryUser : IdentityUser
    {
        [MaxLength(100)]
        public string? FullName { get; set; }
    }
}
