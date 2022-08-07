using System.Net;
using GoRest.Api.Client.Client.Interfaces;
using GoRest.Api.Client.Client.Realization;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Serialization;
using RestEase;

namespace GoRest.Api.Client.Client
{
    public class GoRestClient
    {
        private readonly CookieContainer _cookieContainer = new CookieContainer();

        private GoRestClient() { }

        /// <summary>
        /// Created ApiClient with 'Auth' and 'Referer' headers
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <returns></returns>
        public static T For<T>() where T : ISupportBearerAuth
        {
            var client = new GoRestClient() { };

            var httpClient = client.CreateHttpClient(new Dictionary<string, string>());
            var restClient = client.CreateRestClient<T>(httpClient)
                .AddValidAuthHeader(AppSettings.AuthKey);

            return restClient;
        }   
        public static T ForInvalidToken<T>() where T : ISupportBearerAuth
        {
            var client = new GoRestClient() { };

            var httpClient = client.CreateHttpClient(new Dictionary<string, string>());
            var restClient = client.CreateRestClient<T>(httpClient)
                .AddInValidAuthHeader(AppSettings.AuthKeyInvalid);

            return restClient;
        }  
        public static T ForWithoutToken<T>() where T : ISupportBearerAuth
        {
            var client = new GoRestClient() { };

            var httpClient = client.CreateHttpClient(new Dictionary<string, string>());
            var restClient = client.CreateRestClient<T>(httpClient)
                .WithoutToken();

            return restClient;
        }

        private HttpClient CreateHttpClient(Dictionary<string, string> headers)
        {
            var handler = new HttpClientHandler { UseCookies = true, CookieContainer = _cookieContainer };
            var httpClient = new HttpClient(handler)
            {
                BaseAddress = AppSettings.ApplicationUrl
            };
            foreach (var header in headers) httpClient.DefaultRequestHeaders.Add(header.Key, header.Value);

            return httpClient;
        }

        private T CreateRestClient<T>(HttpClient httpClient)
        {
            var restClient = new RestClient(httpClient)
            {
                JsonSerializerSettings = new JsonSerializerSettings()
                {
                    ContractResolver = new DefaultContractResolver() { NamingStrategy = new LowerCaseNamingStrategy() },
                    Converters = { new StringEnumConverter() }
                }
            };

            var client = restClient.For<T>();
            return client;
        }

    }
}