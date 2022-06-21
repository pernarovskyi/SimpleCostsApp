using System.ComponentModel.DataAnnotations;


namespace CostApplication.Models.Requests
{
    public class RegisterRequest
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public string Password { get; set; }
        
        [Required]
        public string ConfirmPassword { get; set; }
    }
}
