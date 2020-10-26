using System;
using System.Text.Json.Serialization;

namespace MovieTraders.Security
{
    public class AuthUser
    {
        [JsonPropertyName("userId")]
        public int UserId { set; get; }

        [JsonPropertyName("expiresTicks")]
        public long ExpiresTicks { set; get; }

        [JsonPropertyName("token")]
        public string Token { set; get; }

        public DateTime ExpiresDate()
        {
            return new DateTime(ExpiresTicks);
        }
    }
}