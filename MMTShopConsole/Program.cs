using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMTShopConsole
{
  public class Program
  {
    static async Task Main(string[] args)
    {
    Start:
      Console.Clear();
      Console.WriteLine("===================M M T Shop===================");
      Console.WriteLine("================By:Abdul Qader==================");
      Console.WriteLine("================================================");
      Console.WriteLine("Press any number to get your desired results.");
      Console.WriteLine("================================================");
      Console.WriteLine("1-  Add Product");
      Console.WriteLine("2-  Featured Products");
      Console.WriteLine("3-  Available categories");
      Console.WriteLine("4-  Products by category");
      Console.WriteLine("5-  Close program");
      Console.WriteLine("================================================");
      string choice = Console.ReadLine();
      if (int.TryParse(choice, out int choiceinput) == false)
      {
        Console.WriteLine("Only enter a valid number");
        Console.ReadKey();
        goto Start;
      }

      if (choice == "1")
      {
        Console.WriteLine("Enter Product Detail \n");
        await Categories.CategoryList();
        Products.ProductAdd product = new Products.ProductAdd();
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
        await Products.AddProduct(product);
      }
      switch (choice)
      {
        case "1":
          {
            break;
          }
        case "2":
          {
            Console.WriteLine("Enter any Category ID from the list bellow, to get list of all products of its range \n");
            Console.WriteLine("1- Home");
            Console.WriteLine("2- Garden");
            Console.WriteLine("3- Electronics");
          featuredStart:
            var skuchoice = Console.ReadLine();
            if (skuchoice.ToString() == "1" || skuchoice.ToString() == "2" || skuchoice.ToString() == "3")
            {
              Products.ProductsList_byCat(Convert.ToInt32(skuchoice));
            }
            else
            {
              Console.WriteLine("Invalid Inpue");
              goto featuredStart;
            }
            break;
          }
        case "3":
          {
            await Categories.CategoryList();
            break;
          }
        case "4":
          {
            Console.WriteLine("Enter Category ID from list");
            await Categories.CategoryList();
          CategoryInput:
            var catidinput = Console.ReadLine();
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
            Products.ProductsList_byCat(Convert.ToInt32(catidinput));
            break;
          }
        case "5":
          {
            Environment.Exit(0);
            break;
          }
        default:
          {
            Console.WriteLine("Not a valid choice");
            break;
          }
      }

      Console.ReadLine();
      goto Start;
    }
    public static bool isNumber(string inputnumber)
    {
      if (int.TryParse(inputnumber, out int number))
        return true;
      else
        return false;
    }

  }
}