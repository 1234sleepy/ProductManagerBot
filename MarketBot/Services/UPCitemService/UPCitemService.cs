using Newtonsoft.Json;
using ProductManagerBot.Data.Entities;
using System;
using System;
using RestSharp;
using Newtonsoft.Json;
using Microsoft.CSharp;
using ProductManagerBot.DTOs.UpcItems;
using ProductManagerBot.Extensions;

namespace ProductManagerBot.Services.UPCitemService
{
    public class UPCitemService : IUPCitemService
    {
        public async Task<Product?> GetProduct(string barcode)
        {

            var client = new RestClient("https://api.upcitemdb.com/prod/trial/");
            var request = new RestRequest("lookup", Method.GET);
            request.AddQueryParameter("upc", barcode);
            IRestResponse response = client.Execute(request);
            //Console.WriteLine("response: " + response.Content);

            var upcProduct = JsonConvert.DeserializeObject<UpcProduct>(response.Content);
            //Console.WriteLine("offset: " + upcProduct.offset);

            return upcProduct?.items?.FirstOrDefault()?.ToProduct() ;
        }

    }
}
