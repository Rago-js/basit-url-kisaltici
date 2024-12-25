using System;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

class Program
{
    private static string bitlyAccessToken = "YOUR_BITLY_ACCESS_TOKEN";

    static async Task Main(string[] args)
    {
        Console.WriteLine("URL Kısaltıcı'ya Hoş Geldiniz!");
        Console.WriteLine("1. Bitly ile kısalt");
        Console.WriteLine("2. TinyURL ile kısalt");

        string choice = Console.ReadLine();

        Console.Write("Uzun URL'yi gir: ");
        string longUrl = Console.ReadLine();

        string shortUrl = choice switch
        {
            "1" => await GetShortUrlFromBitly(longUrl),
            "2" => await GetShortUrlFromTinyUrl(longUrl),
            _ => throw new Exception("Geçersiz seçim!")
        };

        Console.WriteLine($"Kısaltılmış URL: {shortUrl}");
    }

    static async Task<string> GetShortUrlFromBitly(string longUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "https://api-ssl.bitly.com/v4/shorten";
            client.DefaultRequestHeaders.Add("Authorization", "Bearer " + bitlyAccessToken);

            var content = new StringContent(
                $"{{\"long_url\": \"{longUrl}\"}}", 
                Encoding.UTF8, 
                "application/json"
            );

            HttpResponseMessage response = await client.PostAsync(apiUrl, content);
            response.EnsureSuccessStatusCode();

            string responseBody = await response.Content.ReadAsStringAsync();
            JObject json = JObject.Parse(responseBody);
            return json["link"].ToString();
        }
    }

    static async Task<string> GetShortUrlFromTinyUrl(string longUrl)
    {
        using (HttpClient client = new HttpClient())
        {
            string apiUrl = "http://tinyurl.com/api-create.php?url=" + longUrl;

            HttpResponseMessage response = await client.GetAsync(apiUrl);
            response.EnsureSuccessStatusCode();

            string shortUrl = await response.Content.ReadAsStringAsync();
            return shortUrl;
        }
    }
}
