using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Security
{
    public class ResetPassword : ValidInput
    {
        [Required]
        public int UserId { set; get; }

        [StringLength(16,MinimumLength = 8)]
        [Required(AllowEmptyStrings = false)]
        public string Password { set; get; }

        [StringLength(16, MinimumLength = 8)]
        [Required(AllowEmptyStrings = false)]
        [Compare(nameof(Password))]
        [Display(Name = "Confirm Password")]
        public string ConfirmPassword { set; get; }
    }
}
