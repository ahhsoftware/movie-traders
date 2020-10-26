using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieTraders.Data.Crud.Models;
using MovieTraders.Data.UserDefinedTypes;
using MovieTraders.Functions.Utilities;
using MovieTraders.Security;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;

namespace MovieTraders.Functions
{
    public static class CrudServices
    {
        private const string SERVICE_PROJECT = "MovieTraders.Data";
        private const string SERVICE_NAMESPACE = "MovieTraders.Data.Crud";

        [FunctionName("CrudServices")]
        public static async Task<IActionResult> Run(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/crud/{serviceName}")] HttpRequest request,
            string serviceName,
            ILogger log)
        {

            var service = ServiceFactory.CrudService(log);

            Type serviceType = service.GetType();
            MethodInfo method = serviceType.GetMethod(serviceName, BindingFlags.Public | BindingFlags.Instance);
            if (method == null)
            {
                return new NotFoundObjectResult("The service does not exist");
            }

            if (!Authenticator.UserAuthorized(request.Headers[Constants.AUTH_USER_HEADER], log, out _))
            {
                return new UnauthorizedResult();
            }

            string json = await request.ReadAsStringAsync();

            return Execute(method, serviceName, service, json);
        }
        private static IActionResult Execute(MethodInfo method, string serviceName, Data.Crud.Service service, string json)
        {
            var parameters = method.GetParameters();
            if (parameters.Length != 0)
            {
                var input = GetInputObject(serviceName, json);
                if (((IValidInput)input).IsValid() == false)
                {
                    return new BadRequestObjectResult(input);
                }

                var result = method.Invoke(service, new object[] { input });
                return new OkObjectResult(result);
            }
            else
            {
                var result = method.Invoke(service, null);
                return new OkObjectResult(result);
            }
        }

        private static object GetInputObject(string serviceName, string json)
        {
            Type inputType = Type.GetType($"{SERVICE_NAMESPACE}.Models.{serviceName}Input, {SERVICE_PROJECT}");
            var input = Activator.CreateInstance(inputType);

            if (!string.IsNullOrEmpty(json))
            {
                try
                {
                    if(inputType == typeof(UserMovieMergeInput))
                    {
                        return UserMovieMergeInput(json);
                    }
                    JsonConvert.PopulateObject(json, input);
                }
                catch(Exception ex)
                {
                    Console.WriteLine(ex);
                }
            }
            return input;
        }

        /// <summary>
        /// Since the Formats withing the input object are based on an interface they can't be json deserialized.
        /// There are several ways to handle this but this is the most straightforward and easy to understand.
        private static UserMovieMergeInput UserMovieMergeInput(string json)
        {
            UserMovieMergeInputExt ext = JsonConvert.DeserializeObject<UserMovieMergeInputExt>(json);
            var result = new UserMovieMergeInput
            {
                UserId = ext.UserId,
                MovieId = ext.MovieId,
                Formats = new List<ITinyInts>()
            };
            foreach (var format in ext.Formats)
            {
                result.Formats.Add(new TinyInts { Id = format.Id });
            }
            return result;
        }

        private class UserMovieMergeInputExt : UserMovieMergeInput
        {
            public new List<TinyInts> Formats { set; get; }
        }
    }
}
