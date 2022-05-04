using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoolBooks.ViewModels
{
    public class EditUserViewModel
    {

        [Required]
        [PersonalData]
        public string? Name { get; set; }
        
        [PersonalData]
        public string? UserName { get; set; }
        [PersonalData]
        public DateTime DOB { get; set; }
        public string? ImagePath { get; set; }
        [NotMapped]
        [Display(Name = "Upload File")]
        public IFormFile? ImageFile { get; set; }
        public string PhoneNumber { get; set; }

    }
}
