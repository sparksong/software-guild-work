using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMaster.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models;
using System.IO;
using FlooringMastery.Models.Responses;

namespace FlooringMastery2._0.UI
{
    public class ConsoleIO
    {
        public const string Separator = "**********************************************************************************************************************";

        public static void Header()
        {
            Console.Clear();
            Console.WriteLine(Separator);
            Console.WriteLine();
            Console.WriteLine("Super Special Awesome Flooring Program");
            Console.WriteLine();
            Console.WriteLine(Separator);
            Console.WriteLine();
        }

        public static string GetOrderNameFromUser(string prompt)
        {
            while (true)
            {
                Header();
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Header();
                    Console.WriteLine("Please enter a valid name.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
                else if (input.Length > 19)
                {
                    Console.WriteLine("Name length is too long. Please shorten the name entered.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    return input;
                }
            }
        }

        public static string GetOrderStateFromUser(string prompt)
        {

            while (true)
            {
                Header();
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (TaxRepository.GetTaxRate(input.ToUpper()) == -1)
                {
                    Header();
                    Console.WriteLine("You must enter valid state initials. (MN, OH, PA, MI, IN)");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
                else
                {
                    if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                    {
                        Header();
                        Console.WriteLine("You must enter a state.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                    }
                    else
                    {
                        return input;
                    }
                }
            }
        }

        public static int GetProductFromUser(int userType)
        {
            bool valid = false;

            do
            {
                Header();
                Console.WriteLine("Please choose the flooring you would like for your order:");
                Console.WriteLine("1) Carpet");
                Console.WriteLine("2) Laminate");
                Console.WriteLine("3) Tile");
                Console.WriteLine("4) Wood");

                string userChoice = Console.ReadLine();

                switch (userChoice.ToUpper())
                {
                    case "1":
                    case "CARPET":
                        userType = 1; //ProductType.Carpet;
                        valid = true;
                        break;
                    case "2":
                    case "LAMINATE":
                        userType = 2; //ProductType.Laminate;
                        valid = true;
                        break;
                    case "3":
                    case "TILE":
                        userType = 3; //ProductType.Tile;
                        valid = true;
                        break;
                    case "4":
                    case "WOOD":
                        userType = 4; //ProductType.Wood;
                        valid = true;
                        break;
                    default:
                        Header();
                        Console.WriteLine("Invalid option selected, please choose again.");
                        Console.ReadKey();
                        valid = false;
                        break;
                }
            }
            while (!valid);
            return userType;
        }

        public static DateTime GetOrderDateFromUser(string prompt)
        {
            while (true)
            {
                Header();
                Console.Write(prompt);
                DateTime result;
                string input = Console.ReadLine();

                if (DateTime.TryParse(input, out result))
                {
                    if (result > DateTime.Today)
                    {
                        Header();
                        Console.WriteLine("Order date must be on or before today's date.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        continue;
                    }
                    else
                    {
                        return result;
                    }
                }
                else
                {
                    Header();
                    Console.WriteLine("Please enter the date in the correct format. (MM/DD/YYYY)");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                    continue;
                }
            }
        }

        public static int GetAreaFromUser(string prompt)
        {
            int areaValue;

            while (true)
            {
                Header();
                Console.Write(prompt);
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) ||
                    string.IsNullOrWhiteSpace(input))
                {
                    Header();
                    Console.WriteLine("You must enter a valid area.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
                else if (int.TryParse(input, out areaValue))
                {
                    if (areaValue <= 0)
                    {
                        Header();
                        Console.WriteLine("Please enter a positive number for the area.");
                        Console.WriteLine("Press any key to return.");
                        Console.ReadKey();
                        continue;
                    }

                    return areaValue;
                }
                else
                {
                    Header();
                    Console.WriteLine("You must enter numbers only.");
                    Console.WriteLine("Press any key to return.");
                    Console.ReadKey();
                }
            }
        }

        public static decimal JustTax(decimal taxRate, ProductType Type, int areaSqFt, string state)
        {
            decimal costBeforeTax = Calculations.CalculateCost(Type, areaSqFt);
            taxRate = TaxRepository.GetTaxRate(state);

            return taxRate * costBeforeTax;
        }

        public static decimal GetTotal(decimal taxRate, ProductType Type, int areaSqFt, string state)
        {
            decimal costBeforeTax = Calculations.CalculateCost(Type, areaSqFt);
            taxRate = TaxRepository.GetTaxRate(state);

            decimal tax = taxRate * costBeforeTax;

            return tax + costBeforeTax;
        }

        public static string GetYesNoAnswerFromUser(string prompt)
        {
            while (true)
            {
                Console.Write(prompt + " (Y/N) ");
                string input = Console.ReadLine();

                if (string.IsNullOrEmpty(input) || string.IsNullOrWhiteSpace(input))
                {
                    Console.WriteLine("Please enter Y/N only.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                }
                else
                {
                    if (input.ToUpper() != "Y" && input.ToUpper() != "N")
                    {
                        Console.WriteLine("Please enter Y/N only.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        continue;
                    }
                    return input;
                }
            }
        }

        public static List<Order> PrintOrderList(DateTime orderDate)
        {
            OrderManager orderManager = OrderManagerFactory.Create();
            OrderLookupResponse response = orderManager.LookupOrder(orderDate);

            if (response.Success == false)
            {
                Console.WriteLine("There are no listed orders for {0:d}.", orderDate);

            }
            else
            {
                string line = "{0, -7} {1, -20} {2, -6} {3,-11:d} {4, -10} {5, -8} {6, -11} {7, -10} {8, -8} {9, -8}";
                Console.WriteLine(line, "Order#", "Name", "State", "OrderDate", "Area(Sqft)", "Product", "ProductCost", "LaborCost", "Tax", "Total");
                Console.WriteLine(Separator);
                foreach (var order in response.orders)
                {
                    Console.WriteLine(line, order.OrderNumber, order.Name, order.State, order.OrderDate, order.Area, order.Type, order.CostMaterial, order.CostLabor, order.Tax, order.TotalCost);
                }
            }
            return response.orders;
        }

        public static ProductType GetProductFromUser(ProductType userType)
        {
            bool valid = false;

            do
            {
                ConsoleIO.Header();
                Console.WriteLine("Please choose the flooring type you would like to purchase: ");
                Console.WriteLine("1) Carpet");
                Console.WriteLine("2) Laminate");
                Console.WriteLine("3) Tile");
                Console.WriteLine("4) Wood");

                string userChoice = Console.ReadLine();

                switch (userChoice.ToUpper())
                {
                    case "1":
                    case "CARPET":
                        userType = ProductType.Carpet;
                        valid = true;
                        break;
                    case "2":
                    case "LAMINATE":
                        userType = ProductType.Laminate;
                        valid = true;
                        break;
                    case "3":
                    case "TILE":
                        userType = ProductType.Tile;
                        valid = true;
                        break;
                    case "4":
                    case "WOOD":
                        userType = ProductType.Wood;
                        valid = true;
                        break;
                    default:
                        Console.WriteLine("Invalid option selected, please choose again.");
                        Console.ReadKey();
                        valid = false;
                        break;
                }
            }
            while (!valid);
            return userType;
        }
    }
}