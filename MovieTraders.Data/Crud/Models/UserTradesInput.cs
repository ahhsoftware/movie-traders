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

namespace MovieTraders.Data.Crud.Models
{
    /// <summary>
    /// Interface for UserTradesInput object.
    /// </summary>
    public interface IUserTradesInput : IValidInput
    {
        int? UserId { set; get; }
    }

    /// <summary>
    /// Input object for UserTrades method.
    /// </summary>
    public class UserTradesInput : ValidInput, IUserTradesInput
    {
        [Required(AllowEmptyStrings = false)]
        public int? UserId { set; get; }

    }
} 