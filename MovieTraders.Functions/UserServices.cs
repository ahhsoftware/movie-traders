using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Extensions.Http;
using Microsoft.Extensions.Logging;
using MovieTraders.Data.Extended.Models;
using MovieTraders.Functions.Notifications;
using MovieTraders.Functions.Utilities;
using MovieTraders.Security;
using Newtonsoft.Json;
using System;
using System.Threading.Tasks;

namespace MovieTraders.Functions
{
    
    public static class UserServices
    {
        [FunctionName(nameof(Login))]
        public static async Task<IActionResult> Login(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/user/login")] HttpRequest request,
            ILogger log)
        {
            
            UserLogin login = await GetUserLogin(request);
            if (!login.IsValid())
            {
                return new BadRequestObjectResult(login);
            }

            var output = ServiceFactory.ExtendedService(log).UserForLogin(new UserForLoginInput { Email = login.Email });
            if (output.ReturnValue == UserForLoginOutput.Returns.NotFound)
            {
                return new UnauthorizedResult();
            }

            var user = output.ResultData;
            if (user.LockDate.HasValue)
            {
                if(user.LockReason != Constants.NEW_USER_LOCK && user.LockReason != Constants.CHANGE_PASSWORD_LOCK)
                {
                    return new ForbidResult();
                }
            }

            if (PasswordUtil.IsMatch(login.Password, user.PasswordSalt, user.PasswordHash, user.PasswordIterations))
            {
                DateTime expires = DateTime.Now.AddDays(Constants.PASS_DAYS);

                if (user.LockReason == Constants.NEW_USER_LOCK || user.LockReason == Constants.CHANGE_PASSWORD_LOCK)
                {
                    expires = DateTime.MinValue;
                    ServiceFactory.ExtendedService(log).UserUnlock(new UserUnlockInput { UserId = user.UserId });
                }
                AuthUser authorizedUser = new AuthUser
                {
                    ExpiresTicks = expires.Ticks,
                    UserId = user.UserId
                };
                TokenUtil tokenUtil = new TokenUtil(Settings.TokenString);
                tokenUtil.SetUserToken(authorizedUser);

                return new OkObjectResult(authorizedUser);
            }
            else
            {
                return new UnauthorizedResult();
            }
        }

        [FunctionName(nameof(Register))]
        public static async Task<IActionResult> Register(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/user/register")] HttpRequest request,
            ILogger log)
        {

            if (!Authenticator.UserAuthorized(request.Headers[Constants.AUTH_USER_HEADER], log, out _))
            {
                return new UnauthorizedResult();
            }

            UserRegister register = await GetUserRegister(request);

            if (!register.IsValid())
            {
                return new BadRequestObjectResult(register);
            }

            string randomPassword = RandomPassword.Create();
            PasswordDetails passwordDetails = PasswordUtil.Generate(randomPassword);

            var userInsertOutput = ServiceFactory.ExtendedService(log).UserInsert(new UserInsertInput
            {
                Email = register.Email,
                PasswordHash = passwordDetails.Hash,
                PasswordIterations = passwordDetails.Iterations,
                PasswordSalt = passwordDetails.Salt,
                Phone = register.Phone,
                LockDate = DateTime.Now,
                LockReason = Constants.NEW_USER_LOCK,
                Name = register.Name,
                CountyId = register.CountyId
            });

            if(userInsertOutput.ReturnValue == UserInsertOutput.Returns.Conflict)
            {
                return new ConflictResult();
            }

            EmailService emailService = new EmailService(Settings.EmailSettings);
            emailService.Send(new EmailMessage
            {
                Email = register.Email,
                Name = register.Name,
                Subject = "Movie Traders Registration",
                MessageHtml = EmailBuilder.GetInvitationEmail(register.Name, Settings.EmailSettings.SiteUrl, register.Email, randomPassword)
            });

            return new OkObjectResult(userInsertOutput);
        }

        [FunctionName(nameof(ForgotPassword))]
        public static async Task<IActionResult> ForgotPassword(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/user/forgot-password")] HttpRequest request,
            ILogger log)
        {
            string json = await request.ReadAsStringAsync();
            ForgotPassword forgotPassword = JsonConvert.DeserializeObject<ForgotPassword>(json);

            var userForForgotPasswordOutput = ServiceFactory.ExtendedService(log)
                .UserForForgotPassword(new UserForForgotPasswordInput
                {
                    Email = forgotPassword.Email,
                    Phone = forgotPassword.Phone
                });

            if (userForForgotPasswordOutput.ReturnValue == UserForForgotPasswordOutput.Returns.Ok)
            {
                var user = userForForgotPasswordOutput.ResultData;

                if (user.LockDate.HasValue)
                {
                    if (user.LockReason != Constants.CHANGE_PASSWORD_LOCK)
                    {
                        return new ForbidResult();
                    }
                }

                string password = RandomPassword.Create();
                PasswordDetails pwd = PasswordUtil.Generate(password);
                
                ServiceFactory.ExtendedService(log)
                    .UserSetPassword(new UserSetPasswordInput
                {
                    UserId = user.UserId,
                    PasswordHash = pwd.Hash,
                    PasswordIterations = pwd.Iterations,
                    PasswordSalt = pwd.Salt
                });

                ServiceFactory.ExtendedService(log)
                    .UserLock(new UserLockInput
                {
                    LockReason = Constants.CHANGE_PASSWORD_LOCK,
                    UserId = user.UserId
                });

                EmailService service = new EmailService(Settings.EmailSettings);
                service.Send(new EmailMessage
                {
                    Email = forgotPassword.Email,
                    Name = user.Name,
                    Subject = "Movie Traders Reset Password",
                    MessageHtml = EmailBuilder.GetForgotPasswordEmail(Settings.EmailSettings.FromEmail, forgotPassword.Email, user.Name, password)
                });
            }

            return new OkResult();
        }

        [FunctionName(nameof(ResetPassword))]
        public static async Task<IActionResult> ResetPassword(
            [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "v1/user/reset-password")] HttpRequest request,
            ILogger log)
        {
            string json = await request.ReadAsStringAsync();
            string token = request.Headers[Constants.AUTH_USER_HEADER];

            TokenUtil tokenUtil = new TokenUtil(Settings.TokenString);
            var userFromToken = tokenUtil.UserFromToken(token);

            if (userFromToken == null)
            {
                return new UnauthorizedResult();
            }

            var input = JsonConvert.DeserializeObject<ResetPassword>(json);
            if (!input.IsValid())
            {
                return new BadRequestObjectResult(input);
            }

            if (input.UserId != userFromToken.UserId)
            {
                return new ForbidResult();
            }

            var output = ServiceFactory.ExtendedService(log)
                .UserLockInfo(new UserLockInfoInput { UserId = userFromToken.UserId });

            if(output.ReturnValue == UserLockInfoOutput.Returns.NotFound)
            {
                return new ForbidResult();
            }

            var lockInfo = output.ResultData;
            if(lockInfo.LockDate.HasValue)
            {
                return new ForbidResult();
            }

            PasswordDetails pwd = PasswordUtil.Generate(input.Password);
            var pwdInput = new UserSetPasswordInput
            {
                UserId = input.UserId,
                PasswordHash = pwd.Hash,
                PasswordIterations = pwd.Iterations,
                PasswordSalt = pwd.Salt
            };

            var userSetPasswordOuput = ServiceFactory.ExtendedService(log)
                .UserSetPassword(pwdInput);

            if (userSetPasswordOuput.ReturnValue == UserSetPasswordOutput.Returns.NotFound)
            {
                return new ForbidResult();
            }

            AuthUser authorizedUser = new AuthUser
            {
                UserId = input.UserId,
                ExpiresTicks = DateTime.Now.AddDays(Constants.PASS_DAYS).Ticks
            };
            tokenUtil.SetUserToken(authorizedUser);

            return new OkObjectResult(authorizedUser);
        }

        private static async Task<UserLogin> GetUserLogin(HttpRequest request)
        {
            string json = await request.ReadAsStringAsync();
            if(string.IsNullOrEmpty(json))
            {
                return new UserLogin();
            }
            return JsonConvert.DeserializeObject<UserLogin>(json);
        }

        private static async Task<UserRegister> GetUserRegister(HttpRequest request)
        {
            string json = await request.ReadAsStringAsync();
            if (string.IsNullOrEmpty(json))
            {
                return new UserRegister();
            }

            return JsonConvert.DeserializeObject<UserRegister>(json);
        }
    }
}