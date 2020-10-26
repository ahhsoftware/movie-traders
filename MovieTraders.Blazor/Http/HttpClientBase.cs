using System;
using System.ComponentModel;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace MovieTraders.Blazor.Http
{
    public class HttpClientBase
    {
        protected readonly HttpClient client;
        private readonly JsonSerializerOptions options = new JsonSerializerOptions()
        {
            PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
            IgnoreNullValues = true
        };

        public HttpClientBase(HttpClient client)
        {
            this.client = client ?? throw new ArgumentNullException(nameof(client));
        }

        protected async Task<HttpResult<T>> PostAsync<T>(string urlFragment, object obj)
        {
            var result = new HttpResult<T>();
            try
            {
                var response = await client.PostAsync(urlFragment, GetStringContent(obj));
                await ProcessResult(response, result);
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.NotFound;
                result.ErrorResult = ex.Message;
            }
            return result;
        }

        protected async Task<HttpResult> PostAsync(string urlFragment, object obj)
        {
            var result = new HttpResult();
            try
            {
                var response = await client.PostAsync(urlFragment, GetStringContent(obj));
                result.StatusCode = response.StatusCode;
            }
            catch (Exception ex)
            {
                result.StatusCode = System.Net.HttpStatusCode.NotFound;
                result.ErrorResult = ex.Message;
            }
            return result;
        }


        private async Task ProcessResult<T>(HttpResponseMessage response, HttpResult<T> result)
        {
            result.StatusCode = response.StatusCode;
            string data = await response.Content.ReadAsStringAsync();

            if (result.IsSuccess)
            {
                if (string.IsNullOrEmpty(data))
                {
                    result.Value = default;
                }
                else
                {
                    try
                    {
                        if (typeof(T).IsValueType || typeof(T) == typeof(string))
                        {
                            result.Value = Convert<T>(data);
                        }
                        else
                        {
                            result.Value = JsonSerializer.Deserialize<T>(data, options);
                        }
                    }
                    catch (Exception ex)
                    {
                        result.ErrorResult = $"The call succeed with an HttpStatusCode {result.StatusCode} but an error occured consuming the result with the following exception: {ex}";
                    }
                }
            }
            else
            {
                if (result.StatusCode == System.Net.HttpStatusCode.Unauthorized)
                {
                    result.ErrorResult = "Access Denied";
                }
                else
                {
                    if (string.IsNullOrEmpty(data))
                    {
                        result.ErrorResult = "The call results in an error status but no additional information is available.";
                    }
                    else
                    {
                        result.ErrorResult = data;
                    }
                }
            }
        }

        private T Convert<T>(string input)
        {
            TypeConverter typeConverter = TypeDescriptor.GetConverter(typeof(T));
            if (typeConverter != null)
            {
                return (T)typeConverter.ConvertFromString(input);
            }
            return default;
        }

        private StringContent GetStringContent(Object obj)
        {
            return new StringContent(GetJson(obj), System.Text.Encoding.UTF8, "application/json");
        }

        private string GetJson(object obj)
        {
            if (obj == null)
            {
                return "{}";
            }
            else
            {
                string json = JsonSerializer.Serialize(obj, options);
                return json;
            }
        }
    }
}
