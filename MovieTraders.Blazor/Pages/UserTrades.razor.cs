using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data.Crud.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class UserTrades : Common
    {
        public void ShowNotes(int tradeId)
        {
            SelectedTradeIdForNotes = tradeId;
            SelectedTradeIdForMovies = 0;
        }
        private void ShowMovies(int tradeId)
        {
            SelectedTradeIdForMovies = tradeId;
            SelectedTradeIdForNotes = 0;
        }

        protected override async Task OnInitializedAsync()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
                return;
            }

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Crud<UserTradesOutput>(new UserTradesInput { UserId = State.User.UserId });
            if (httpResult.IsSuccess)
            {
                output = httpResult.Value;
            }
            else
            {
                await Alert($"Error: {httpResult.ErrorResult}");
            }
        }

        private UserTradesOutput output;
        private int SelectedTradeIdForNotes = 0;
        private int SelectedTradeIdForMovies = 0;
    }
}
namespace MovieTraders.Data.Crud.Models
{
    public partial class UserTradesResult
    {
        public List<Movies> MoviesForTrade(int tradeId)
        {
            return MoviesResult.FindAll(m => m.TradeId == tradeId).ToList();
        }
    }
}
