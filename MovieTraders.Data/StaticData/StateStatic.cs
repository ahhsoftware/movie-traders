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
        public static List<State> StateList = new List<State>()
        {
            new State((byte)1, @"AL", @"Alabama"),
            new State((byte)2, @"AK", @"Alaska"),
            new State((byte)3, @"AZ", @"Arizona"),
            new State((byte)4, @"AR", @"Arkansas"),
            new State((byte)5, @"CA", @"California"),
            new State((byte)6, @"CO", @"Colorado"),
            new State((byte)7, @"CT", @"Connecticut"),
            new State((byte)8, @"DE", @"Delaware"),
            new State((byte)9, @"FL", @"Florida"),
            new State((byte)10, @"GA", @"Georgia"),
            new State((byte)11, @"HI", @"Hawaii"),
            new State((byte)12, @"ID", @"Idaho"),
            new State((byte)13, @"IL", @"Illinois"),
            new State((byte)14, @"IN", @"Indiana"),
            new State((byte)15, @"IA", @"Iowa"),
            new State((byte)16, @"KS", @"Kansas"),
            new State((byte)17, @"KY", @"Kentucky"),
            new State((byte)18, @"LA", @"Louisiana"),
            new State((byte)19, @"ME", @"Maine"),
            new State((byte)20, @"MD", @"Maryland"),
            new State((byte)21, @"MA", @"Massachusetts"),
            new State((byte)22, @"MI", @"Michigan"),
            new State((byte)23, @"MN", @"Minnesota"),
            new State((byte)24, @"MS", @"Mississippi"),
            new State((byte)25, @"MO", @"Missouri"),
            new State((byte)26, @"MT", @"Montana"),
            new State((byte)27, @"NE", @"Nebraska"),
            new State((byte)28, @"NV", @"Nevada"),
            new State((byte)29, @"NH", @"New Hampshire"),
            new State((byte)30, @"NJ", @"New Jersey"),
            new State((byte)31, @"NM", @"New Mexico"),
            new State((byte)32, @"NY", @"New York"),
            new State((byte)33, @"NC", @"North Carolina"),
            new State((byte)34, @"ND", @"North Dakota"),
            new State((byte)35, @"OH", @"Ohio"),
            new State((byte)36, @"OK", @"Oklahoma"),
            new State((byte)37, @"OR", @"Oregon"),
            new State((byte)38, @"PA", @"Pennsylvania"),
            new State((byte)39, @"RI", @"Rhode Island"),
            new State((byte)40, @"SC", @"South Carolina"),
            new State((byte)41, @"SD", @"South Dakota"),
            new State((byte)42, @"TN", @"Tennessee"),
            new State((byte)43, @"TX", @"Texas"),
            new State((byte)44, @"UT", @"Utah"),
            new State((byte)45, @"VT", @"Vermont"),
            new State((byte)46, @"VA", @"Virginia"),
            new State((byte)47, @"WA", @"Washington"),
            new State((byte)48, @"WV", @"West Virginia"),
            new State((byte)49, @"WI", @"Wisconsin"),
            new State((byte)50, @"WY", @"Wyoming"),
        };
    }

    public class State
    {
        public State(byte StateId, string Abbreviation, string Name)
        {
            this.StateId = StateId;
            this.Abbreviation = Abbreviation;
            this.Name = Name;
        }

        public byte StateId  { get; }
        public string Abbreviation  { get; }
        public string Name  { get; }
    }
}
