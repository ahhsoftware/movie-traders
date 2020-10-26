using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data.Crud.Models;
using System.Threading.Tasks;
using System.Web;

namespace MovieTraders.Blazor.Pages
{
    public partial class MovieInsert : Common
    {
        private readonly MovieInsertInput input = new MovieInsertInput();

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
                return;
            }
            input.CreatedByUserId = State.User.UserId;
            await Task.Delay(0);
        }

        private async Task ValidSubmit()
        {
            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<MovieInsertOutput>(input);
            if (httpResult.IsSuccess)
            {
                Navigation.NavigateTo($"searchmovies/{HttpUtility.UrlEncode(input.Title)}");
            }
            else
            {
                await Alert($"Error: {httpResult.ErrorResult}");
            }
        }
    }
}
