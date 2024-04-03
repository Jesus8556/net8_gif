using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class LinkExtractor
{
    private readonly HttpClient _httpClient;
    private const string ApiKey = "LIVDSRZULELA";
    private const string SearchTerm = "excited";

    public LinkExtractor()
    {
        _httpClient = new HttpClient();
    }

    public async Task<List<Link>> ExtractLinksAsync(int limit = 8)
    {
        var url = $"https://g.tenor.com/v1/search?q={SearchTerm}&key={ApiKey}&limit={limit}";

        try
        {
            var response = await _httpClient.GetAsync(url);
            response.EnsureSuccessStatusCode(); // Throws exception if request fails
            var content = await response.Content.ReadAsStringAsync();

            dynamic gifData = JsonConvert.DeserializeObject(content);
            var links = new List<Link>();

            foreach (var gif in gifData.results)
            {
                if (gif.media != null && gif.media.Count > 0 && gif.media[0].webm != null && gif.media[0].webm.preview != null)
                {
                    links.Add(new Link
                    {
                        Text = gif.id,
                        Href = gif.media[0].webm.preview.ToString()
                    });
                }
            }

            return links;
        }
        catch (HttpRequestException e)
        {
            Console.WriteLine($"Error al realizar la solicitud: {e.Message}");
            return new List<Link>();
        }
    }
}

public class Link
{
    public string Text { get; set; }
    public string Href { get; set; }
}
