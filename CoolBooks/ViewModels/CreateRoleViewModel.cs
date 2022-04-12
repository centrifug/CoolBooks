using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;

namespace CoolBooks.ViewModels
{
    public class CreateRoleViewModel
    {
        [Required]
        [Display(Name = "Role")]
        public string RoleName { get; set; }

        public List<IdentityRole> Roles { get; set; } = new List<IdentityRole>();
    }
}
