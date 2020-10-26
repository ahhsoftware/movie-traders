using Microsoft.AspNetCore.Components;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data;
using MovieTraders.Data.Crud.Models;
using MovieTraders.Data.UserDefinedTypes;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class UserMovieMerge : Common
    {
        [Parameter]
        public int MovieId { set; get; }

        private UserMovieQueryResult.Movies Movie = new UserMovieQueryResult.Movies();
        private readonly UserMovieMergeInput input = new UserMovieMergeInput();

        private readonly List<SelectedFormat> SelectedFormats = new List<SelectedFormat>();

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
                return;
            }

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<UserMovieQueryOutput>(new UserMovieQueryInput { MovieId = MovieId, UserId = State.User.UserId });
            if (httpResult.IsSuccess)
            {
                var output = httpResult.Value;
                if(output.ReturnValue == UserMovieQueryOutput.Returns.Ok)
                {
                    Movie = output.ResultData.MoviesResult[0];
                    input.UserId = State.User.UserId;
                    input.MovieId = MovieId;
                    BuildSelectList(output.ResultData.UserMoviesResult);
                }
                else
                {
                    await Alert("The movie selected is no longer available");
                }
            }
            else
            {
                await Alert($"Error: {httpResult.ErrorResult}");
            }
        }

        private void BuildSelectList(List<UserMovieQueryResult.UserMovies> userMovies)
        {
            foreach(var format in StaticData.FormatList)
            {
                SelectedFormat selected = new SelectedFormat
                {
                    FormatId = format.FormatId,
                    Name = format.Name
                };

                if (userMovies != null && userMovies.Count != 0)
                {
                    if (userMovies.Find(f => f.FormatId == format.FormatId) != null)
                    {
                        selected.Selected = true;
                    }
                }
                SelectedFormats.Add(selected);
            }
        }

        public async Task ValidSubmit()
        {
            Saving = true;

            input.Formats = new List<ITinyInts>();
            foreach (var format in SelectedFormats)
            {
                if(format.Selected)
                {
                    input.Formats.Add(new TinyInts { Id = format.FormatId });
                }
            }

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<UserMovieMergeOutput>(input);

            if(httpResult.IsSuccess)
            {
                Navigation.NavigateTo("/usermovies");
            }
            else
            {
                await Alert(httpResult.ErrorResult);
            }
            Saving = false;
        }
        private class SelectedFormat
        {
            public byte FormatId { set; get; }

            public string Name { set; get; }

            public bool Selected { set; get; }
        }
    }
}
