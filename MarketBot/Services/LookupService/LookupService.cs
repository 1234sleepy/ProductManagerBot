using ProductManagerBot.Data.Entities;
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
                //if (File.Exists("cookie.txt"))
                //{
                //    File.ReadAllText("cookie.txt")
                //        .Split('\n')
                //        .Select(x =>
                //        {
                //            var cookieData = x.Split("=");
                //            return new Cookie(cookieData[0], cookieData[1], "/", "barcodelookup.com");
                //        }).ToList().ForEach(coockie => cookieContainer.Add(coockie));

                //}
                //else
                {
                    cookieContainer.Add(new Cookie("cf_clearance", "cJGeMmjAjtcet.N_1cffsiurhYaoGI4mGVE6fP2b4J0-1703447927-0-2-7c3054b2.3e1d2dff.66da7a02-0.2.1703447927", "/", ".barcodelookup.com"));

                    cookieContainer.Add(new Cookie("_ga_6K9HJQ9YDK", "GS1.1.1703447925.5.1.1703447926.0.0.0", "/", ".barcodelookup.com"));

                    cookieContainer.Add(new Cookie("bl_csrf", "1e2f6cb61e00c023b46115d4b514d19f", "/", ".barcodelookup.com"));

                    cookieContainer.Add(new Cookie("__cflb", "04dToRCegghj9KSg7BqsUc4efEezbNiZ67xwfzK8JR", "/", "www.barcodelookup.com"));

                    cookieContainer.Add(new Cookie("bl_session", "d8tgf1mgicne8d93tmbbkm8am43ef2bt", "/", ".barcodelookup.com"));

                    cookieContainer.Add(new Cookie("_ga", "GA1.1.859256291.1702841831", "/", ".barcodelookup.com"));

                    cookieContainer.Add(new Cookie("__cf_bm", "wqeR_lRpzUYwCwTZ9LqmcCxOICsiPhiJ1PM22Q5PfU4-1703447926-1-ATiCnNBoRcaDMFhNdbG18pOgSu0VuC93M8u/QhhfYLLrho8DF5EtdemU387F5TKe74Va7DxUSUQ8Db/IfwdV1xOs829Awzq/qoFsixaGDFTu", "/", ".barcodelookup.com"));
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
                File.WriteAllText("page.html", page);

                Uri uri = new Uri($"https://www.barcodelookup.com/{barcode}");
                var responseCookie = cookieContainer.GetCookies(uri).Cast<Cookie>();

                File.WriteAllText("cookie.txt", string.Join("\n", responseCookie));

                int indexOfEditButton = page.IndexOf("edit-product-btn");
                int startIndex = page.IndexOf("<h4>", indexOfEditButton);
                int endIndex = page.IndexOf("</h4>", startIndex);
                string name = page.Substring(startIndex + 4, endIndex - startIndex - 4).Trim();


                int manufOfEditButton = page.IndexOf("Manufacturer");
                int startmanuf = page.IndexOf("Manufacturer:", manufOfEditButton);
                int endmanuf = page.IndexOf("Manufacturer:", startIndex);
                var manufacturer = new Manufacture { Name = page.Substring(startmanuf + 28, endmanuf - startmanuf - 7).Trim() };


                int kcalOfEditButton = page.IndexOf("Manufacturer");
                int kcal = 0;
                if (kcalOfEditButton > 0)
                {

                    int startkcal = page.IndexOf("Nutrition Facts:", kcalOfEditButton);
                    int endkcalr = page.IndexOf("Nutrition Facts:", startIndex);
                    kcal = int.Parse(page.Substring(startmanuf + 37, endmanuf - startmanuf - 121).Trim());
                }

                int indexOfBar = page.IndexOf(" Barcode Formats:");
                startIndex = page.IndexOf("<span>", indexOfEditButton);
                endIndex = page.IndexOf("</span>", startIndex);
                string barcoder = page.Substring(startIndex + 7, endIndex - startIndex - 7).Trim();

                int indexOfCategory = page.IndexOf("Category:");

                Category category = null;
                if (indexOfCategory > 0)
                {
                    startIndex = page.IndexOf("<span>", indexOfEditButton);
                    endIndex = page.IndexOf("</span>", startIndex);
                    category = new Category { Name = page.Substring(startIndex + 7, endIndex - startIndex - 7).Trim() };
                }

                return new Product 
                { 
                    Name = name, 
                    Barcode = barcoder, 
                    Category = category, 
                    Manufacture = manufacturer, 
                    Calories = kcal 
                };

            }
            catch
            {

            }
            return null;
        }
    }
}
