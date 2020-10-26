using Microsoft.AspNetCore.Components;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data.Crud.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;

namespace MovieTraders.Blazor.Pages
{
    public partial class SearchMovies : Common
    {
        private readonly MovieQueryInput input = new MovieQueryInput();
        private List<MovieQueryResult> Movies = null;

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
            }

            if (!string.IsNullOrEmpty(Title))
            {
                input.Title = HttpUtility.UrlDecode(Title);
                await ValidSubmit();
            }
        }


        [Parameter]
        public string Title { set; get; }
        public async Task ValidSubmit()
        {
            Saving = true;

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<MovieQueryOutput>(input);

            if (httpResult.IsSuccess)
            {
                var output = httpResult.Value;
                if(output.ReturnValue == MovieQueryOutput.Returns.NotFound)
                {
                    Movies = new List<MovieQueryResult>();
                }
                else
                {
                    Movies = output.ResultData;
                }
            }
            else
            {
                await Alert("Error");
                await Alert(httpResult.ErrorResult);
            }

            Saving = false;
        }
    }
}
