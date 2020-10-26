using Microsoft.JSInterop;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Security;
using System;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class Index : Common
    {
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                if (State.User == null)
                {
                    try
                    {
                        string json = await JavaScript.InvokeAsync<string>("statemanager.get", nameof(AuthUser));

                        if (!string.IsNullOrEmpty(json))
                        {
                            State.User = System.Text.Json.JsonSerializer.Deserialize<AuthUser>(json);
                            Navigation.NavigateTo("/");
                        }
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine(e);
                    }
                }
            }
        }
    }
}
