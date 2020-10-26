using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Security
{
    public class UserLogin : ValidInput
    {
        [Required]
        [EmailAddress]
        [StringLength(64)]
        public string Email { set; get; }

        [Required]
        [StringLength(16, MinimumLength = 8)]
        public string Password { set; get; }
    }
}
