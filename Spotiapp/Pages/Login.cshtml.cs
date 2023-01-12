using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using Spotiapp.Models;
using System.Net.Http.Headers;
using Spotiapp.Services;

namespace Spotiapp.Pages
{
    public class LoginModel : PageModel
    {
        private readonly ISpotifyAuthServices _spotifyAuthServices;
        private readonly ISpotifyServices _spotifyServices;

        private string code;
        public LoginModel(
            ISpotifyServices spotifyServices,
            ISpotifyAuthServices spotifyAuthServices)
        {
            _spotifyAuthServices = spotifyAuthServices;
            _spotifyServices = spotifyServices;
        }

        [ViewData]
        public IEnumerable<Test> _listRest { get; set; }

        public async Task<IActionResult> OnGet()
        {
            //SpotifyToken token = await _spotifyAuthServices.GetAccessToken("e76c18b063df4c56853ed5db4a0cf92f", "45a00136aec44d24b31d177b160ff56f");

            //SpotifyResponse spotifyResponse = await _spotifyServices.GetAlbums(token.access_token, "US", 20, 1);
            var test = _spotifyAuthServices.GetAcessCode(
                "e76c18b063df4c56853ed5db4a0cf92f",
                "code",
                "https://localhost:7157/Privacy",
                "user-read-private user-read-email");

            //var listRes = spotifyResponse.albums.items.Select(item =>
            //new Test
            //{
            //    name = item.name,
            //    release_date = item.release_date,
            //    img_url = item.images.FirstOrDefault().url,
            //    link = item.external_urls.spotify,
            //    artists = string.Join(",", item.artists.Select(i => i.name))
            //}
            //);
            //_listRest = listRes;

            return Redirect(test.Result);


        }


    }
}
