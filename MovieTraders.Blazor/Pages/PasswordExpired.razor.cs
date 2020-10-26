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
    public partial class PasswordExpired : Common
    {

        [Parameter]
        public string RedirectUrl { set; get; }

        private readonly ResetPassword input = new ResetPassword();

        protected override async Task OnInitializedAsync()
        {
            try
            {
                string json = await JavaScript.InvokeAsync<string>("statemanager.get", nameof(Security.AuthUser));

                if (string.IsNullOrEmpty(json))
                {
                    Navigation.NavigateTo("/");
                }

                State.User = System.Text.Json.JsonSerializer.Deserialize<AuthUser>(json);
                input.UserId = State.User.UserId;
            }

            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }

        public async Task ValidSubmit()
        {
            var client = Client.GetClient(State.User.Token);

            var httpResult = await client.ResetPassword(input);

            if (httpResult.IsSuccess)
            {
                State.User = httpResult.Value;

                await JavaScript.InvokeAsync<string>("statemanager.set", nameof(AuthUser), System.Text.Json.JsonSerializer.Serialize(State.User));
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
                FormError = "Service refused the action. Only the verified user can reset the password.";
            }
            else
            {
                FormError = httpResult.ErrorResult;
            }
        }
    }
}
