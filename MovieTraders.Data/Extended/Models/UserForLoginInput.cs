// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace MovieTraders.Data.Extended.Models
{
    /// <summary>
    /// Interface for UserForLoginInput object.
    /// </summary>
    public interface IUserForLoginInput : IValidInput
    {
        string Email { set; get; }
    }

    /// <summary>
    /// Input object for UserForLogin method.
    /// </summary>
    public class UserForLoginInput : ValidInput, IUserForLoginInput
    {
        /// <summary>
        /// Email
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Email { set; get; }

    }
} 