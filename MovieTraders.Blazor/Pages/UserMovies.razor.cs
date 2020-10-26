using Microsoft.JSInterop;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data.Crud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class UserMovies : Common
    {
        private List<UserMovie> Movies = new List<UserMovie>();

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
                return;
            }

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<UserMovieQueryOutput>(new UserMovieQueryInput { UserId = State.User.UserId });
            if (httpResult.IsSuccess)
            {
                var output = httpResult.Value;
                BuildMovieList(output.ResultData.MoviesResult, output.ResultData.UserMoviesResult);
            }
            else
            {
                await Alert($"Error: {httpResult.ErrorResult}");
            }
        }

        private void BuildMovieList(List<UserMovieQueryResult.Movies> movies, List<UserMovieQueryResult.UserMovies> userMovies)
        {
            Movies = new List<UserMovie>();

            if (movies != null && movies.Count != 0)
            {
                foreach (var movie in movies)
                {
                    UserMovie userMovie = new UserMovie(movie);
                    if (userMovies.Find(u => u.MovieId == userMovie.MovieId && u.FormatId == 1) != null)
                    {
                        userMovie.DVD = true;
                    }

                    if (userMovies.Find(u => u.MovieId == userMovie.MovieId && u.FormatId == 2) != null)
                    {
                        userMovie.BlueRay = true;
                    }

                    if (userMovies.Find(u => u.MovieId == userMovie.MovieId && u.FormatId == 3) != null)
                    {
                        userMovie.VHS = true;
                    }
                    Movies.Add(userMovie);
                }
            }
            else
            {
                movies = new List<UserMovieQueryResult.Movies>();
            }
        }
        public class UserMovie : UserMovieQueryResult.Movies
        {
            public UserMovie(UserMovieQueryResult.Movies movie)
            {
                GenreId = movie.GenreId;
                Link = movie.Link;
                MovieId = movie.MovieId;
                Title = movie.Title;
                Year = movie.Year;
            }
            public bool DVD { set; get; }
            public bool BlueRay { set; get; }
            public bool VHS { set; get; }
        }
    }
}
