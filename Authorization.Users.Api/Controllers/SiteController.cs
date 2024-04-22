using IdentityModel.Client;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;

namespace Authorization.Users.Api.Controllers
{
    [Route("[controller]")]
    public class SiteController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SiteController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        [Route("[action]")]
        
        public IActionResult Index()
        {
            return View();
        }

        [Route("[action]")]
        public async Task<IActionResult> GetOrders()
        {
            // retrieve to IdentityServer
            var authClient = _httpClientFactory.CreateClient();
            var discoveryDocument = await authClient.GetDiscoveryDocumentAsync("https://localhost:10001");

            var tokenResponse = await authClient.RequestClientCredentialsTokenAsync(new ClientCredentialsTokenRequest()
            {
                Address = discoveryDocument.TokenEndpoint,

                ClientId = "client_id",
                ClientSecret = "client_secret",

                Scope = "OrdersAPI"
            });

            // retrieve to Orders
            var ordersClient = _httpClientFactory.CreateClient();

            ordersClient.SetBearerToken(tokenResponse.AccessToken);

            var responce = await ordersClient.GetAsync("https://localhost:7151/site/secret");
            
            if(!responce.IsSuccessStatusCode)
            {
                ViewBag.Message = responce.StatusCode.ToString();
                return View();
            }

            var message = await responce.Content.ReadAsStringAsync();

            ViewBag.Message = message;
            return View();
        }
    }
}
