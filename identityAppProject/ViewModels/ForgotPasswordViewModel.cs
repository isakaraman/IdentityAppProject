using System.ComponentModel.DataAnnotations;

namespace identityAppProject.ViewModels
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
