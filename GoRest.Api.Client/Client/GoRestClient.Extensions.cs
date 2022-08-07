using GoRest.Api.Client.Client.Interfaces;

namespace GoRest.Api.Client.Client
{
    public static class GoRestClientExtensions
    {
        public static T WithoutToken<T>(this T client) where T : ISupportBearerAuth
        {
            client.AuthHeader = null;
            return client;
        }

        public static T AddValidAuthHeader<T>(this T client, string token) where T : ISupportBearerAuth
        {
            client.AuthHeader = $"Bearer {token}";
            return client;
        }
        public static T AddInValidAuthHeader<T>(this T client, string token) where T : ISupportBearerAuth
        {
            client.AuthHeader = $"Bearer {token}";
            return client;
        }
    }
}