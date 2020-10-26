using MovieTraders.Blazor.Http;
using MovieTraders.Blazor.LocalServices;
using MovieTraders.Blazor.PageBase;
using MovieTraders.Data;
using MovieTraders.Data.Extended.Models;
using MovieTraders.Security;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Pages
{
    public partial class UserInvite : Common
    {
        private readonly UserRegisterExt input = new UserRegisterExt();

        protected override void OnInitialized()
        {
            if (State.User == null)
            {
                Navigation.NavigateTo("/");
            }
        }
        private async Task ValidSubmit()
        {
            input.Phone = new string(input.Phone.Where(char.IsDigit).ToArray());

            Saving = true;

            var client = Client.GetClient(State.User.Token);
            var httpResult = await client.Register((UserRegister)input);
            if (httpResult.IsSuccess)
            {
                Navigation.NavigateTo("/");
            }
            else
            {
                if(httpResult.StatusCode == System.Net.HttpStatusCode.Conflict)
                {
                    FormError = "The phone or email has previously been used";
                }
                else
                {
                    await Alert(httpResult.ErrorResult);
                }
                
            }

            Saving = false;
        }

    }

    public class UserRegisterExt: UserRegister
    {
        private int stateId = 0;
        public int StateId
        {
            get
            {
                return stateId;
            }
            set
            {
                stateId = value;
                Counties = Data.StaticData.CountyList.FindAll(c => c.StateId == StateId);
            }
        }

        public List<Data.State> States = StaticData.StateList;

        public List<Data.County> Counties { set; get; }

    }
}
