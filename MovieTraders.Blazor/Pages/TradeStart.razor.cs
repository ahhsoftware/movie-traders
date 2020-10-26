using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data;
using MovieTraders.Data.Crud.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class TradeStart : Common
    {
        private List<MoviesByCountyResult> Movies;

        private int PageCount = 1;

        private async Task GoToPage(int pageNumber)
        {
            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<MoviesByCountyOutput>(new MoviesByCountyInput { UserId = State.User.UserId, PageNumber = pageNumber, PageSize=25});

            if (httpResult.IsSuccess)
            {
                var output = httpResult.Value;
                if (output.ReturnValue == MoviesByCountyOutput.Returns.Ok)
                {
                    Movies = output.ResultData;
                    PageCount = output.PageCount.Value;
                }
            }
            else
            {
                await Alert("Error");
                await Alert(httpResult.ErrorResult);
            }
        }
        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
            }

            await GoToPage(1);
        }
    }
}
