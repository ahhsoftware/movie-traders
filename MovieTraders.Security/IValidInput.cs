using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Security
{
    interface IValidInput
    {
        List<ValidationResult> ValidationResults { set; get; }
        bool IsValid();
    }
    public class ValidInput : IValidInput
    {
        public List<ValidationResult> ValidationResults { set; get; } = new List<ValidationResult>();

        public virtual bool IsValid()
        {
            Validator.TryValidateObject(this, new ValidationContext(this), ValidationResults, true);
            return ValidationResults.Count == 0;
        }
    }
}
