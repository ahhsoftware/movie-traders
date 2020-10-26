using Microsoft.AspNetCore.Components;
using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.PageBase;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class ForgotPassword : Common
    {
        [Parameter]
        public string RedirectUrl { set; get; }

        private readonly Security.ForgotPassword input = new Security.ForgotPassword();

        public async Task ValidSubmit()
        {
            Saving = true;

            input.Phone = new string(input.Phone.Where(char.IsDigit).ToArray());

            var client = Client.GetClient();
            var httpResult = await client.ForgotPassword(input);

            if (httpResult.IsSuccess)
            {
                AlternateContentMessage = "If there is an account associated with the information provided you will receive an email with instructions for resetting your password";
            }
            else
            {
                FormError = httpResult.ErrorResult;
            }

            Saving = false;
        }
    }
}
