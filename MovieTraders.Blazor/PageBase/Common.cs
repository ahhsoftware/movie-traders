using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MovieTraders.Blazor.LocalServices;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.PageBase
{
    public class Common : ComponentBase
    {
        [Inject]
        protected NavigationManager Navigation { set; get; }

        [Inject]
        protected IJSRuntime JavaScript { set; get; }

        [Inject]
        protected State State { set; get; }

        protected string FormError { set; get; }

        protected bool Saving { set; get; } = false;

        public string AlternateContentMessage { set; get; } = null;

        public async Task DisplayNav()
        {
            string[] navs = new string[] { "nav-home", "nav-usermovies", "nav-trades", "nav-logout", "nav-userinvite" };
            foreach (string nav in navs)
            {
                await JavaScript.InvokeAsync<object>("statemanager.setVisibility", nav, true);
            } 
        }

        public async Task Alert(string message)
        {
            await JavaScript.InvokeAsync<object>("alert", message);
        }

        public async Task<bool> Confirm(string message)
        {
            return await JavaScript.InvokeAsync<bool>("confirm", message);
        }
    }
}
