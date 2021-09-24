using System;
using System.Net.Http;
using System.Net.Http.Headers;

namespace MMTShopConsole
{
  public abstract class ApiHelper
  {
    public static HttpClient ApiClient { get; set; }
    public static string baseurl { get; set; }

    public static void InitializeClient()
    {
      ApiClient = new HttpClient();

      string baseUrl = $"https://localhost:44301/Api/";

      ApiClient.DefaultRequestHeaders.Accept.Clear();
      ApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
      ApiClient.BaseAddress = new Uri(baseUrl);
    }
  }
}
