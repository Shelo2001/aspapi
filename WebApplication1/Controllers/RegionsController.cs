using Microsoft.AspNetCore.Mvc;
using System.Net.Http;
using System.Reflection;
using System.Text;
using System.Text.Json;
using WebApplication1.Models;
using WebApplication1.Models.DTO;

namespace WebApplication1.Controllers
{
    public class RegionsController : Controller
    {
        private readonly IHttpClientFactory httpClientFactory;

        public RegionsController(IHttpClientFactory httpClientFactory)
        {
            this.httpClientFactory = httpClientFactory;
        }

        public async Task<IActionResult> Index()
        {
            List<RegionsDto> response = new List<RegionsDto>();

            var client  = httpClientFactory.CreateClient();
            
            var httpResponseMessage = await client.GetAsync("https://localhost:7055/api/Regions");

            httpResponseMessage.EnsureSuccessStatusCode();

            response.AddRange(await httpResponseMessage.Content.ReadFromJsonAsync<IEnumerable<RegionsDto>>());


            return View(response);
        }

        [HttpGet]
        public  IActionResult Add()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Add(AddRegionViewModel model)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequestMessage = new HttpRequestMessage()
            {
                Method = HttpMethod.Post,
                RequestUri = new Uri("https://localhost:7055/api/Regions"),
                Content = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(httpRequestMessage);

            await httpResponse.Content.ReadFromJsonAsync<RegionsDto>();

            if (Response is not null)
            {
                return RedirectToAction("Index", "Regions");
            }
            return View();


        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponse = await client.GetFromJsonAsync<RegionsDto>($"https://localhost:7055/api/Regions/{id.ToString()}");

            if (httpResponse is not null)
            {
                return View(httpResponse);
            }
            return View(null);

        }

        [HttpPost]
        public async Task<IActionResult> Edit(RegionsDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpRequest = new HttpRequestMessage()
            {
                Method = HttpMethod.Put,
                RequestUri = new Uri($"https://localhost:7055/api/Regions/{request.Id}"),
                Content = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json")
            };

            var httpResponse = await client.SendAsync(httpRequest);
            httpResponse.EnsureSuccessStatusCode();

            var response = await httpResponse.Content.ReadFromJsonAsync<RegionsDto>();

            if (response is not null)
            {
                return RedirectToAction("Edit", "Regions");
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Delete(RegionsDto request)
        {
            var client = httpClientFactory.CreateClient();

            var httpResponse = await client.DeleteAsync($"https://localhost:7055/api/Regions/{request.Id}");

            httpResponse.EnsureSuccessStatusCode();

            return RedirectToAction("Index", "Regions");
    

        }
    }
}
