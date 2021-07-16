using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;
using Bff.Api.Dtos;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

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
            var response = await JsonSerializer.DeserializeAsync<Customer>(customerApiResponse, new JsonSerializerOptions { PropertyNameCaseInsensitive = true});

            return response;
        }
    }
}