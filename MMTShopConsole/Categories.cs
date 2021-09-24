using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMTShopConsole
{
  public class Categories
  {
    public class Category
    {
      public int CatID { get; set; }
      public string Name { get; set; }
      public string SkuValue { get; set; }
    }

    public static async Task CategoryList()
    {
      string url = $"Categories/";
      ApiHelper.InitializeClient();
      using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
      {
        try
        {
          if (response.IsSuccessStatusCode)
          {
            string result = await response.Content.ReadAsStringAsync();

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(result);

            foreach (Category c in categories)
            {
              Console.WriteLine("ID: " + c.CatID + "| Name: " + c.Name + "| SKUValue: " + c.SkuValue);
            }
          }
          else
          {
            Console.WriteLine("failed: " + response);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
    }
    public static async Task<bool> IsCategoryExist(int catid)
    {
      bool outcomevalue = false;

      string url = $"Categories/";
      using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
      {
        try
        {
          if (response.IsSuccessStatusCode)
          {
            string result = await response.Content.ReadAsStringAsync();

            List<Category> categories = JsonConvert.DeserializeObject<List<Category>>(result);

            foreach (Category c in categories)
            {
              if (c.CatID == catid)
              {
                outcomevalue = true;
                break;
              }
            }
          }
          else
          {
            Console.WriteLine("failed: " + response);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      return outcomevalue;
    }
    public static async Task<string> CategoryName(int catid)
    {
      string outcomevalue = "";

      string url = $"Categories/" + catid;
      using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
      {
        try
        {
          if (response.IsSuccessStatusCode)
          {
            string result = await response.Content.ReadAsStringAsync();

            Category categories = JsonConvert.DeserializeObject<Category>(result);
            outcomevalue = categories.Name;

          }
          else
          {
            Console.WriteLine("failed: " + response);
          }
        }
        catch (Exception ex)
        {
          Console.WriteLine(ex.Message);
        }
      }
      return outcomevalue;
    }
  }
}
