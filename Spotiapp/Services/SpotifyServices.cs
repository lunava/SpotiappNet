using System;
using System.Net.Http.Headers;
using System.Text.Json;
using Spotiapp.Models;

namespace Spotiapp.Services
{
	public class SpotifyServices :ISpotifyServices
	{
		private readonly IHttpClientFactory _httpClientFactory;

		public SpotifyServices(IHttpClientFactory httpClientFactory)
		{
            _httpClientFactory = httpClientFactory;
		}

        public async Task<SpotifyResponse> GetAlbums(string AccessToken, string countryCode, int limit, int offset)
        {
           var  _httpClient = _httpClientFactory.CreateClient("Services");
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", AccessToken);
            var response = await _httpClient.GetAsync($"browse/new-releases?country={countryCode}&limit={limit}&offset={offset}");

            using var responseStream = await response.Content.ReadAsStreamAsync();

            var spotifyResponse = await JsonSerializer.DeserializeAsync<SpotifyResponse>(responseStream);
            Console.WriteLine($"Service Test: {spotifyResponse.albums.items[0]}");
            return spotifyResponse;
        }
    }
}

