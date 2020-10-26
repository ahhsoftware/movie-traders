using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Security;
using System;
using System.Threading.Tasks;
using System.Web;

namespace MovieTraders.Blazor.Pages
{
    public partial class Login : Common
    {
        [Parameter]
        public string RedirectUrl { set; get; }

        private readonly UserLogin input = new UserLogin();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string json = await JavaScript.InvokeAsync<string>("statemanager.get", nameof(AuthUser));

                if (!string.IsNullOrEmpty(json))
                {
                    State.User = System.Text.Json.JsonSerializer.Deserialize<AuthUser>(json);
                    if (State.User.ExpiresDate() <= DateTime.Today)
                    {
                        Navigation.NavigateTo($"/passwordexpired/{HttpUtility.UrlEncode(RedirectUrl)}");
                    }
                    else
                    {
                        await DisplayNav();

                        if (string.IsNullOrEmpty(RedirectUrl))
                        {
                            Navigation.NavigateTo("/");
                        }
                        else
                        {
                            Navigation.NavigateTo(HttpUtility.UrlDecode(RedirectUrl));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task ValidSubmit()
        {
            Saving = true;

            var client = Client.GetClient();
            var httpResult = await client.Login(input);

            if (httpResult.IsSuccess)
            {
                State.User = httpResult.Value;
               
                await JavaScript.InvokeAsync<string>("statemanager.set", nameof(AuthUser), System.Text.Json.JsonSerializer.Serialize(State.User));
                
                if (State.User.ExpiresDate() <= DateTime.Now)
                {
                    Navigation.NavigateTo($"/passwordexpired/{HttpUtility.UrlEncode(RedirectUrl)}");
                    return;
                }
                
                await DisplayNav();

                if (!string.IsNullOrEmpty(RedirectUrl))
                {
                    Navigation.NavigateTo(HttpUtility.UrlDecode(RedirectUrl));
                }
                else
                {
                    Navigation.NavigateTo("/");
                }
            }
            else if (httpResult.StatusCode == System.Net.HttpStatusCode.Unauthorized)
            {
                FormError = "The Email/Password combination is not valid.";
            }
            else if (httpResult.StatusCode == System.Net.HttpStatusCode.Forbidden)
            {
                FormError = "The user account is locked";
            }
            else
            {
                FormError = httpResult.ErrorResult;
            }

            Saving = false;
        }
    }
}