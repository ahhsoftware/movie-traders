﻿// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------

using System.Collections.Generic;

namespace MovieTraders.Data
{
    public partial class StaticData
    {
        public static List<Format> FormatList = new List<Format>()
        {
            new Format((byte)1, @"DVD"),
            new Format((byte)2, @"Blue Ray"),
            new Format((byte)3, @"VHS"),
        };
    }

    public class Format
    {
        public Format(byte FormatId, string Name)
        {
            this.FormatId = FormatId;
            this.Name = Name;
        }

        public byte FormatId  { get; }
        public string Name  { get; }
    }
}
