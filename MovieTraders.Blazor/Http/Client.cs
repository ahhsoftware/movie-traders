using MovieTraders.Data.Extended.Models;
using MovieTraders.Security;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Http
{
    public class Client : HttpClientBase
    {

        #region Conditional

#if DEBUG
        private const string baseUrl = "http://localhost:7071";
#else
        private const string baseUrl = "";
#endif
        #endregion

        public Client(HttpClient client) : base(client) { }

        public static Client GetClient()
        {
            HttpClient client = new HttpClient
            {
                BaseAddress = new Uri(baseUrl)
            };
            return new Client(client);
        }

        public static void SetHeader(Client client, string token)
        {
            client.client.DefaultRequestHeaders.Add(Constants.AUTH_USER_HEADER, token);
        }

        public static Client GetClient(string token)
        {
            Client client = GetClient();
            SetHeader(client, token);
            return client;
        }

        public async Task<HttpResult<AuthUser>> Login(UserLogin user)
        {
            return await PostAsync<AuthUser>("api/v1/user/login", user);
        }

        public async Task<HttpResult<UserInsertOutput>> Register(UserRegister user)
        {
            return await PostAsync<UserInsertOutput>("/api/v1/user/register", user);
        }

        public async Task<HttpResult<AuthUser>> ResetPassword(ResetPassword input)
        {
            return await PostAsync<AuthUser>("/api/v1/user/reset-password", input);
        }

        public async Task<HttpResult> ForgotPassword(ForgotPassword input)
        {
            return await PostAsync("/api/v1/user/forgot-password", input);
        }

        public async Task<HttpResult<T>> Crud<T>(object input)
        {
            string serviceName = typeof(T).Name.Replace("Output", "");
            return await (PostAsync<T>($"/api/v1/crud/{serviceName}", input));
        }

        public async Task<HttpResult<T>> Crud<T>()
        {
            return await Crud<T>(null);
        }
    }
}