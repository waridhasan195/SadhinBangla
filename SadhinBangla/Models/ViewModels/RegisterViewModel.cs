using System.ComponentModel.DataAnnotations;

namespace SadhinBangla.Models.ViewModels
{
    public class RegisterViewModel
    {
        [Required]
        public string UserName { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        [MinLength(6, ErrorMessage ="Password has to be at last 6 characters.")]
        public string Password { get; set; }
    }
}
