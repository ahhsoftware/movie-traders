using NUnit.Framework;
using System;

namespace MovieTraders.Security.Tests
{
    public class AllTests
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void TestToken()
        {
            TokenUtil tokenUtil = new TokenUtil("EF8cCkj23PLqmddeisn7u1wd");
            bool error = false;

            for(int idx=0; idx!= 100000; idx++)
            {
                long ticks = idx * 2;
                AuthUser userFromValues = new AuthUser()
                {
                    ExpiresTicks = ticks,
                    UserId = idx
                };

                tokenUtil.SetUserToken(userFromValues);

                AuthUser userFromToken = tokenUtil.UserFromToken(userFromValues.Token);

                if(userFromValues.UserId != userFromToken.UserId)
                {
                    Console.WriteLine("UserId mismatch");
                    error = true;
                    break;
                }

                if(userFromValues.ExpiresTicks != userFromToken.ExpiresTicks)
                {
                    Console.WriteLine("ExpiresTicks mismatch");
                    error = true;
                    break;
                }

                if(userFromValues.ExpiresDate() != userFromToken.ExpiresDate())
                {
                    Console.WriteLine("ExpiresDate() mismatch");
                    error = true;
                    break;
                }
            }

            if(error)
            {
                Assert.Fail();
            }
        }
    }
}