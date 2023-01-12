using System;
using Spotiapp.Models;

namespace Spotiapp.Services
{
	public interface ISpotifyServices
	{
        public Task<SpotifyResponse> GetAlbums(
            string AccessToken,
            string countryCode,
            int limit,
            int offset);
    }
}

