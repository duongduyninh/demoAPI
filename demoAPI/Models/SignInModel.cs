using System.ComponentModel.DataAnnotations;

namespace demoAPI.Models
{
    public class SignInModel
    {
        [Required , EmailAddress]
        public string Email { get; set; } = null!;
        [Required]
        public string Passsword { get; set; } = null!;
    }
}
