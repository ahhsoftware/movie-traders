// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
namespace MovieTraders.Data.Extended.Models
{
    /// <summary>
    /// Result object for UserForForgotPassword routine.
    /// </summary>
    public partial class UserForForgotPasswordResult
	{
        public int UserId { set; get; }
        public string Name { set; get; }
        public DateTime? LockDate { set; get; }
        public string LockReason { set; get; }
    }
}