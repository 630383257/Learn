using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Sun.MVCClient.Models;
using IdentityModel;
using IdentityModel.Client;
using System.Net.Http;
namespace Sun.MVCClient.Controllers
{
    public class InfoController : Controller
    {
        public async Task<TokenResponse> GetToken()
        {
            var disco = await DiscoveryClient.GetAsync("http://localhost:49173");
            var tokenClient = new TokenClient(disco.TokenEndpoint, "client", "Secret");
            var tokenResponse = await tokenClient.RequestClientCredentialsAsync("api1");

            if (tokenResponse.IsError)
            {
                return null;
            }


            return tokenResponse;
        }

        public async Task<IActionResult> Index()
        {
            var client = new HttpClient();
            var tokenResponse = await GetToken();
            client.SetBearerToken(tokenResponse.AccessToken);

            var response = await client.GetAsync("http://localhost:49559/Info");
            if (!response.IsSuccessStatusCode) return BadRequest();
            else
            {
                var content = await response.Content.ReadAsStringAsync();
                return Json(content);
            }
        }
    }
}
