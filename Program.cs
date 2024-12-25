using System;
using System.Collections.Generic;

class Program
{

    static Dictionary<string, string> urlDatabase = new Dictionary<string, string>();
    static Random random = new Random();

    static void Main()
    {
        Console.WriteLine("URL Kısaltıcı'ya Hoş Geldiniz!");

        while (true)
        {
            Console.WriteLine("\n1. URL Kısalt\n2. Kısaltılmış URL'yi Görüntüle\n3. Çıkış\nSeçim yap: ");
            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.Write("Uzun URL'yi gir: ");
                    string longUrl = Console.ReadLine();
                    string shortUrl = CreateShortUrl(longUrl);
                    Console.WriteLine($"Kısaltılmış URL: {shortUrl}");
                    break;

                case "2":
                    Console.Write("Kısaltılmış URL'yi gir: ");
                    string inputShortUrl = Console.ReadLine();
                    if (urlDatabase.TryGetValue(inputShortUrl, out string originalUrl))
                    {
                        Console.WriteLine($"Orijinal URL: {originalUrl}");
                    }
                    else
                    {
                        Console.WriteLine("Bu URL bulunamadı!");
                    }
                    break;

                case "3":
                    Console.WriteLine("Çıkış yapılıyor...");
                    return;

                default:
                    Console.WriteLine("Geçersiz seçim, tekrar deneyin.");
                    break;
            }
        }
    }

    static string CreateShortUrl(string longUrl)
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
        string shortUrl;

         do
        {
            shortUrl = "https://short.ly/" + new string(Enumerable.Repeat(chars, 6)
                .Select(s => s[random.Next(s.Length)]).ToArray());
        } while (urlDatabase.ContainsKey(shortUrl));

      urlDatabase[shortUrl] = longUrl;
        return shortUrl;
    }
}
