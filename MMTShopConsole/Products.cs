using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace MMTShopConsole
{
  public class Products
  {
    public class Product
    {
      public int ID { get; set; }
      public int CatID { get; set; }
      public string SKU { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public double Price { get; set; }
    }
    public class ProductAdd
    {
      public int ID { get; set; }
      public int CatID { get; set; }
      public string Name { get; set; }
      public string Description { get; set; }
      public double Price { get; set; }
    }

    public static async Task AddProduct(ProductAdd product)
    {
      string url = $"Products/";
      // HTTP POST

      using (HttpResponseMessage response = await ApiHelper.ApiClient.PostAsJsonAsync(url, product))
      {
        try
        {
          if (response.IsSuccessStatusCode)
          {
            Console.WriteLine("Product added successfully...");
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
    public static async void ProductInput()
    {
      Console.WriteLine("Enter Product Detail \n");
      await Categories.CategoryList();
      ProductAdd product = new ProductAdd();
      Console.WriteLine("Enter Category ID from list");
      var catidinput = Console.ReadLine();
    CategoryInput:
      if (!Program.isNumber(catidinput))
      {
        Console.WriteLine("Enter a valid Number");
        goto CategoryInput;
      }
      else if (!(Categories.IsCategoryExist(Convert.ToInt32(catidinput)).Result))
      {
        Console.WriteLine("Category ID not valid, please enter a valid entry");
        goto CategoryInput;
      }
      product.CatID = Convert.ToInt32(catidinput);
      Console.WriteLine("Enter Product Name:");
      product.Name = Console.ReadLine();
      Console.WriteLine("Enter Product Description");
      product.Description = Console.ReadLine();
      Console.WriteLine("Enter Product Price");
    PriceInpue:
      double price;
      if (!(double.TryParse(Console.ReadLine(), out price)))
      {
        Console.WriteLine("Enter a valid number");
        goto PriceInpue;
      }
      product.Price = price;
      await AddProduct(product);
    }

    public static async void ProductsList_byCat(int catid)
    {
      string url = $"Products?categoryid=" + catid;
      ApiHelper.InitializeClient();
      using (HttpResponseMessage response = await ApiHelper.ApiClient.GetAsync(url))
      {
        try
        {
          if (response.IsSuccessStatusCode)
          {
            string result = await response.Content.ReadAsStringAsync();

            List<Product> products = JsonConvert.DeserializeObject<List<Product>>(result);

            foreach (Product p in products)
            {
              string categoryname = Categories.CategoryName(p.CatID).Result;
              Console.WriteLine("ID: " + p.ID);
              Console.WriteLine("Category: " + categoryname);
              Console.WriteLine("Name: " + p.Name);
              Console.WriteLine("SKU: " + p.SKU);
              Console.WriteLine("Description: " + p.Description);
              Console.WriteLine("Price: " + p.Price);
              Console.WriteLine("\n--------------------\n");
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

  }
}
