namespace GoRest.Api.Client
{
    public class AppSettings
    {
        public static Uri ApplicationUrl => new("https://gorest.co.in/public-api/");
        public static string AuthKey => "1471c9cece25b11a29b5cd36f0a956dad30e0f1823df6175a11840878e2c59e8";
        public static string AuthKeyEmpty => null;
        public static string AuthKeyInvalid => "14715999599959999b5cd36f0a956dad30e0f1823df6175a11840878e2c5999";
    }
}