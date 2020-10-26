using System;
using System.Text;

namespace MovieTraders.Security
{
    public class TokenUtil
    {
        private readonly byte[] xor;
        
        public TokenUtil(string key)
        {
            if(string.IsNullOrEmpty(key))
            {
                throw new ArgumentNullException(nameof(key));
            }

            xor = Encoding.ASCII.GetBytes(key);

            if(xor.Length < 24)
            {
                throw new ArgumentOutOfRangeException(nameof(key), "Key must be at least 24 characters");
            }
        }

        public void SetUserToken(AuthUser user)
        {
            byte[] bytes = new byte[16];
            Buffer.BlockCopy(BitConverter.GetBytes(user.UserId), 0, bytes, 0, 4);
            Buffer.BlockCopy(BitConverter.GetBytes(user.ExpiresTicks), 0, bytes, 4, 8);
            Buffer.BlockCopy(BitConverter.GetBytes(GenerateHash(user)), 0, bytes, 12, 4);

            for (int idx = 0; idx != bytes.Length; idx++)
            {
                bytes[idx] = (byte)(xor[idx % 24] ^ bytes[idx]);
            }

            user.Token = Convert.ToBase64String(bytes).Replace('+', '.').Replace('/', '_').Replace('=', '-');
        }

        public int GenerateHash(AuthUser user)
        {
            return ((long)user.UserId | user.ExpiresTicks).GetHashCode();
        }

        public AuthUser UserFromToken(string token)
        {
            if (!string.IsNullOrEmpty(token))
            {
                byte[] bytes = Convert.FromBase64String(token.Replace('.', '+').Replace('_', '/').Replace('-', '='));

                for (int idx = 0; idx != bytes.Length; idx++)
                {
                    bytes[idx] = (byte)(bytes[idx] ^ xor[idx % 24]);
                }

                var user = new AuthUser
                {
                    UserId = BitConverter.ToInt32(bytes, 0),
                    ExpiresTicks = BitConverter.ToInt64(bytes, 4)
                };

                SetUserToken(user);

                int passedHash = BitConverter.ToInt32(bytes, 12);
                int currentHash = GenerateHash(user);

                if (passedHash == currentHash)
                {
                    return user;
                }
            }

            return null;
        }
    }
}