using System.Net.Http;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using Bff.Api.Dtos;
using Microsoft.AspNetCore.Mvc;

namespace Bff.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IHttpClientFactory _clientFactory;

        public CustomersController(IHttpClientFactory clientFactory)
        {
            _clientFactory = clientFactory;
        }

        [HttpGet("{id}")]
        public async Task<Customer> Get(int id)
        {
            var request = new HttpRequestMessage(HttpMethod.Get, $"{id}");
            var client = _clientFactory.CreateClient("customers");
            var customerGetResponse = await client.SendAsync(request);

            customerGetResponse.EnsureSuccessStatusCode();

            var customerApiResponse = await customerGetResponse.Content.ReadAsStreamAsync();
            var response = await JsonSerializer.DeserializeAsync<Customer>(customerApiResponse,
                new JsonSerializerOptions { PropertyNameCaseInsensitive = true });

            return response;
        }

        [HttpPost]
        public async Task<ActionResult> Post(CreateCustomer command)
        {
            var client = _clientFactory.CreateClient("customers");

            var customerGetResponse = await client.PostAsJsonAsync("/", command);

            customerGetResponse.EnsureSuccessStatusCode();

            return StatusCode(202);
        }
    }
}