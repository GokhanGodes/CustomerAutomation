using CustomerAutomationWeb.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;

namespace CustomerAutomationWeb.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly HttpClient _client;
        private string uri = "https://localhost:44368/api/Customer/";
        public CustomerService(HttpClient client)
        {
            _client = client;
        }
        public bool Create(CustomerAddInput cusAdd)
        {
            var response = _client.PostAsJsonAsync($"http://localhost:5010/api/Customer/Add", cusAdd).Result;
            return response.IsSuccessStatusCode;
        }

        public bool Delete(int id)
        {
            var response = _client.PostAsJsonAsync($"http://localhost:5010/api/Customer/Delete", new { id = id }).Result;
            return response.IsSuccessStatusCode;
        }

        public CustomerViewModel Get(int id)
        {
            var response = _client.GetAsync($"http://localhost:5010/api/Customer/Get?id={id}").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            var responseSuccess = response.Content.ReadFromJsonAsync<ResponseMessages<CustomerViewModel>>().Result;
            return responseSuccess.Data;

        }
        public List<CustomerViewModel> GetAll()
        {
            var response = _client.GetAsync($"http://localhost:5010/api/Customer/GetAll").Result;
            if (response.StatusCode != System.Net.HttpStatusCode.OK)
            {
                return null;
            }
            var responseSuccess = response.Content.ReadFromJsonAsync<ResponseMessages<List<CustomerViewModel>>>().Result;
            return responseSuccess.Data;

        }

        public List<CustomerViewModel> Search(CustomerFilterInput cusFilDto)
        {
            var response = _client.GetAsync($"http://localhost:5010/api/Customer/Search?Name={cusFilDto.Name}&LastName={cusFilDto.LastName}&TCKN={cusFilDto.TCKN}").Result;
            var responseSuccess = response.Content.ReadFromJsonAsync<ResponseMessages<List<CustomerViewModel>>>().Result;
            return responseSuccess.Data;
        }
    }
}
