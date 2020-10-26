namespace MovieTraders.Security
{
    public class PasswordDetails
    {
        public PasswordDetails(byte[] salt, byte[] hash, int iterations)
        {
            Salt = salt;
            Hash = hash;
            Iterations = iterations;
        }
        public byte[] Salt { get; }

        public byte[] Hash { get; }

        public int Iterations { get; }
    }
}
