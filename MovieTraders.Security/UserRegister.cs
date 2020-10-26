using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Security
{
    public class UserRegister : ValidInput
    { 
        [StringLength(16)]
        [Phone]
        [Required(AllowEmptyStrings = false)]
        public string Phone { set; get; }

        [EmailAddress]
        [StringLength(64)]
        [Required(AllowEmptyStrings = false)]
        public string Email { set; get; }

        [StringLength(64)]
        [Required(AllowEmptyStrings = false)]
        public string Name { set; get; }

        [Required]
        [Display(Name = "County")]
        public short CountyId { set; get; }

    }
}