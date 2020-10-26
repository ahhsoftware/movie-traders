using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Security
{
    public class ForgotPassword : ValidInput
    {
        [EmailAddress]
        [StringLength(64)]
        [Required]
        public string Email { set; get; }

        [StringLength(16)]
        [Phone]
        [Required]
        public string Phone { set; get; }
    }
}
