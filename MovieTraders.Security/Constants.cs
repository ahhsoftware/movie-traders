namespace MovieTraders.Security
{
    public class Constants
    {
        public const string NEW_USER_LOCK = nameof(NEW_USER_LOCK);

        public const string CHANGE_PASSWORD_LOCK = nameof(CHANGE_PASSWORD_LOCK);
        
        public const int PASS_DAYS = 30;

        public const string AUTH_USER_HEADER = "movie-traders-user";
    }
}
