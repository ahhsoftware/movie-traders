// ------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by SqlPlus.net
//     For more information on SqlPlus.net visit http://www.SqlPlus.net
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
// ------------------------------------------------------------------------------
using System.Collections.Generic;
namespace MovieTraders.Data.Crud.Models
{
    /// <summary>
    /// Result objects for UserMovieQuery routine.
    /// </summary>
    public partial class UserMovieQueryResult
	{
            public List<UserMovies> UserMoviesResult { set; get; }
            public class UserMovies
            {
                public int MovieId { set; get; }
                public byte FormatId { set; get; }
            }
            public List<Movies> MoviesResult { set; get; }
            public class Movies
            {
                public int MovieId { set; get; }
                public byte GenreId { set; get; }
                public int Year { set; get; }
                public string Title { set; get; }
                public string Link { set; get; }
            }
    }
}