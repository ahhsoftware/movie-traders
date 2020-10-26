using Microsoft.AspNetCore.Components;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data.Crud.Models;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class TradeInitialize : Common
    {
        [Parameter]
        public int UserId { set; get; }

        [Parameter]
        public int UserMovieId { set; get; }

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
            }

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<UserMovieQueryOutput>(new UserMovieQueryInput { UserId = UserId});

            if (httpResult.IsSuccess)
            {
                var output = httpResult.Value;
            }
            else
            {
                await Alert("Error");
                await Alert(httpResult.ErrorResult);
            }
        }
    }

}
