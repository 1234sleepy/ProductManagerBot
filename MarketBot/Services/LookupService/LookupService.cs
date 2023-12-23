using ProductManagerBot.Data.Entities;
using System.IO;
using System.Net;

namespace ProductManagerBot.Services.LookupService
{
    public class LookupService : ILookupService
    {
        public async Task<Product> GetProduct(string barcode)
        {
            var cookieContainer = new CookieContainer();
            HttpClientHandler handler = new HttpClientHandler()
            {
                CookieContainer = cookieContainer
            };
            using HttpClient client = new HttpClient(handler);
            try
            {
                if (File.Exists("cookie.txt"))
                {
                    string text = File.ReadAllText("cookie.txt");
                    var all = text.Split('\n').ToArray();
                    foreach(var i in all) {
                        var words = i.Split('=').ToArray();
                        cookieContainer.Add(new Cookie(words[0], words[1], "/", "images.barcodelookup.com"));
                    }
                    
                }
                else
                {
                    cookieContainer.Add(new Cookie("__cflb", "0H28vbcrHzeMmZ5NNrCpT6p7tARu4XNiMmMSkjeETaM", "/", "images.barcodelookup.com"));
                    cookieContainer.Add(new Cookie("_ga_6K9HJQ9YDK", "GS1.1.1703358513.2.1.1703358520.0.0.0", "/", ".barcodelookup.com"));
                    cookieContainer.Add(new Cookie("cf_clearance", "SRSMFRaHm.7h1R59qXos87UChNXnB0qKVMICU2UqnFM-1703358514-0-2-7c3054b2.3e1d2dff.66da7a02-0.2.1703358514", "/", ".barcodelookup.com"));
                    cookieContainer.Add(new Cookie("bl_csrf", "1e2f6cb61e00c023b46115d4b514d19f", "/Session", ".barcodelookup.com"));
                    cookieContainer.Add(new Cookie("__cflb", "04dToRCegghj9KSg7BqsUc4efEezbNiMRggcoD9m9F", "/", "www.barcodelookup.com"));
                    cookieContainer.Add(new Cookie("bl_session", "70is7jq0psgfioc4loja9qrcngv9k3hh", "/", ".barcodelookup.com"));
                    cookieContainer.Add(new Cookie("_ga", "GA1.1.859256291.1702841831", "/", ".barcodelookup.com"));
                    cookieContainer.Add(new Cookie("__cf_bm", "ozy.qmCbmbrdFHXq1kbSYuS9Tr9rF2TMf_PC6TYNBhU-1703358512-1-AedPE6BNsZtu0IPduZHUFg5ZxrQRX7K1scLqwK7SfYyNpIy5FHH+LmhsvC/cdbatVCxasLKD1yaauq9zx1LUIzuSfs804w7bECFTuv8Dv5Hv", "/", ".barcodelookup.com"));

                }
                HttpRequestMessage request = new(HttpMethod.Get, $"https://www.barcodelookup.com/{barcode}");
                request.Headers.Add("Accept-Language", "en-US,en;q=0.9,ru-RU;q=0.8,ru;q=0.7,uk;q=0.6");
                request.Headers.Add("Cache-Control", "no-cache");
                request.Headers.Add("Pragma", "no-cache");
                request.Headers.Add("Sec-Ch-Ua", "\"Not_A Brand\";v=\"8\", \"Chromium\";v=\"120\", \"Google Chrome\";v=\"120\"");
                request.Headers.Add("Sec-Ch-Ua-Mobile", "?0");
                request.Headers.Add("Sec-Ch-Ua-Platform", "\"Windows\"");
                request.Headers.Add("Sec-Fetch-Dest", "document");
                request.Headers.Add("Sec-Fetch-Mode", "navigate");
                request.Headers.Add("Sec-Fetch-Site", "same-origin");
                request.Headers.Add("Sec-Fetch-User", "?1");
                request.Headers.Add("Upgrade-Insecure-Requests", "1");
                request.Headers.Add("User-Agent", "Mozilla/5.0 (Windows NT 10.0; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/120.0.0.0 Safari/537.36");


                var response = await client.SendAsync(request);

                var page = await response.Content.ReadAsStringAsync();
                //var res = response.Headers.
                //File.WriteAllText("page.html", page);

                Uri uri = new Uri($"https://www.barcodelookup.com/{barcode}");
                var responseCookie = cookieContainer.GetCookies(uri).Cast<Cookie>();

                File.WriteAllText("cookie.txt", string.Join("\n", responseCookie));

                int indexOfEditButton = page.IndexOf("edit-product-btn");
                int startIndex = page.IndexOf("<h4>", indexOfEditButton);
                int endIndex = page.IndexOf("</h4>", startIndex);
                string name = page.Substring(startIndex+4, endIndex - startIndex-4).Trim();



                return new Product { Name = name };
            }
            catch
            {

            }
            return null;
        }
    }
}
