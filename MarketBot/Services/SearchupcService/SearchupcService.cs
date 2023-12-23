using Newtonsoft.Json;
using ProductManagerBot.Data.Entities;
using ProductManagerBot.Helpers;
using ProductManagerBot.Services.SearchupcService;

namespace ProductManagerBot.Services.APITokenService
{
    public class SearchupcService : ISearchupcService
    {
        public async Task <Product> GetProduct(String barcode)
        {
            HttpClient httpClient = new HttpClient();

            HttpResponseMessage response = await httpClient.GetAsync($"http://www.searchupc.com/handlers/upcsearch.ashx?request_type=3&access_token=F57BCA88-B4E4-40C2-97F7-10FDBD5EB247&upc=upc_code ");
            response.EnsureSuccessStatusCode();
            string responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine(responseBody);
            Product config = JsonConvert.DeserializeObject<Product>(responseBody);




            return config;
        }
    }
}
