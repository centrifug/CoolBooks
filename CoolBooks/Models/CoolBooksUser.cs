using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.Models
{
    public class CoolBooksUser : IdentityUser
    {
        [Required]
        [PersonalData]
        public string? Name { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
    }
}
