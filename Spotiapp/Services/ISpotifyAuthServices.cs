using System;
using Spotiapp.Models;

namespace Spotiapp.Services
{
	public interface ISpotifyAuthServices
	{
		//Get Access Token from Client Credentials
		public Task<SpotifyToken> GetAccessToken(
			string ClientID,
			string ClientSecret);

		//Get AccessCode from Authorization

		public Task<string> GetAcessCode(
			string ClientID,
			string ResponseType,
			string RedirectUri,
			string Scope = "",
			string ShowDia = ""
			);
		public Task<SpotifyToken> GetAcessToken(
			string code,
			string RedirectUri,
			string ClientID,
			string CleintSecret
			);
	}
}

