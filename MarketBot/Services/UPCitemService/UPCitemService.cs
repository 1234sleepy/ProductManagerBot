using Newtonsoft.Json;
using ProductManagerBot.Data.Entities;
using System;
using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.CSharp;

namespace ProductManagerBot.Services.UPCitemService
{
    public class UPCitemService : IUPCitemService
    {
        public async Task<Product> GetProduct(string barcode)
        {
            var user_key = "only_for_dev_or_pro";

            var client = new RestClient("https://api.upcitemdb.com/prod/trial/");
            var request = new RestRequest("lookup", Method.GET);
            request.AddQueryParameter("upc", barcode);
            IRestResponse response = client.Execute(request);
            Console.WriteLine("response: " + response.Content);

            var obj = JsonConvert.DeserializeObject(response.Content);
            Console.WriteLine("offset: " + obj);

            

            return new Product { };
        }

    }
}
