using Microsoft.Extensions.Logging;
using MovieTraders.Data.Extended.Models;
using MovieTraders.Security;
using System;

namespace MovieTraders.Functions.Utilities
{
    public class Authenticator
    {

        public static bool UserAuthorized(string token, ILogger log, out AuthUser tokenUser)
        {
            TokenUtil tokenUtil = new TokenUtil(Environment.GetEnvironmentVariable("TokenString"));
            tokenUser = tokenUtil.UserFromToken(token);
            if (tokenUser == null)
            {
                return false;
            }

            //get user from db
            var output = ServiceFactory.ExtendedService(log).UserLockInfo(new UserLockInfoInput { UserId = tokenUser.UserId });
            if (output.ReturnValue == UserLockInfoOutput.Returns.NotFound)
            {
                return false;
            }

            var dbUser = output.ResultData;

            //make sure the user is not locked
            if (dbUser.LockDate.HasValue)
            {
                return false;
            }

            var outputUser = new AuthUser
            {
                UserId = tokenUser.UserId,
                ExpiresTicks = tokenUser.ExpiresTicks
            };

            tokenUser = outputUser;

            return true;
        }
    }
}
