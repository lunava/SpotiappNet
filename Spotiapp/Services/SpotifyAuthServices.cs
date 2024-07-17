using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using Spotiapp.Models;

namespace Spotiapp.Services
{
	public class SpotifyAuthServices : ISpotifyAuthServices
	{
		private readonly IHttpClientFactory _httpClientFactory;

        public SpotifyAuthServices(IHttpClientFactory httpClientFactory)
		{
			_httpClientFactory = httpClientFactory;
		}

		public async Task<SpotifyToken> GetAccessToken(string ClientID, string ClientSecret) {
			var _httpClient = _httpClientFactory.CreateClient("Authentication");
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Post,
                "api/token"
                );
            httpRequestMessage.Headers.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes(ClientID + ":" + ClientSecret)));

            httpRequestMessage.Content = new FormUrlEncodedContent(new Dictionary<string, string>()
            {
                { "grant_type", "client_credentials" }
            });
            var response = await _httpClient.SendAsync(httpRequestMessage);


            using var responseStream = await response.Content.ReadAsStreamAsync();
            var token = await JsonSerializer.DeserializeAsync<SpotifyToken>(responseStream);
            Console.WriteLine($"Access Token : {token?.access_token}");
            return token;

        }

       public async Task<string> GetAcessCode(string ClientID, string ResponseType, string RedirectUri, string Scope, string ShowDia)
        {
            var _httpClient = _httpClientFactory.CreateClient("Authentication");

            var httpRequestMessageQuery = QueryHelpers.AddQueryString("",new Dictionary<string, string?>()
                {   {"response_type", "code" },
                    {"client_id", ClientID},
                    {"scope", Scope },
                    {"redirect_uri", RedirectUri },
                    {"state", "randomState" }
                }
            );

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get,
            "authorize" + httpRequestMessageQuery.ToString()
            );
            var response = await _httpClient.SendAsync(httpRequestMessage);

            var reponseString  =  await response.Content.ReadAsStringAsync();

            Console.WriteLine($"Reponse:{httpRequestMessageQuery}");
            return httpRequestMessageQuery;
        }

        public async Task<SpotifyToken> GetAcessToken(string code, string RedirectUri, string ClientID, string CleintSecret)
        {
            throw new NotImplementedException();
        }


    }

}

