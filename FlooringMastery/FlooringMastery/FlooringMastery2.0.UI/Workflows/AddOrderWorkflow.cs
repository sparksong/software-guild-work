using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMaster.BLL;
using FlooringMastery.Models;
using FlooringMastery.Data;
using FlooringMastery.Models.Responses;

namespace FlooringMastery2._0.UI.Workflows
{
    public class AddOrderWorkflow
    {
        public void Execute()
        {
            Order newOrder = new Order();
            bool orderComplete = true;

            do
            {
                newOrder.Name = ConsoleIO.GetOrderNameFromUser("Please enter a name for the order: ");
                newOrder.State = ConsoleIO.GetOrderStateFromUser("Please enter your state initials: ");
                newOrder.TaxRate = TaxRepository.GetTaxRate(newOrder.State);
                newOrder.OrderDate = ConsoleIO.GetOrderDateFromUser("Please enter your order date (MM/DD/YYYY): ");
                newOrder.Type = ConsoleIO.GetProductFromUser(newOrder.Type);
                newOrder.Area = ConsoleIO.GetAreaFromUser("Please enter the area amount you want to purchase: ");

                newOrder.CostLabor = Math.Round(Calculations.GetLaborCost(newOrder.Type, newOrder.Area), 2, MidpointRounding.AwayFromZero);
                newOrder.CostMaterial = Math.Round(Calculations.GetProductCost(newOrder.Type, newOrder.Area), 2, MidpointRounding.AwayFromZero);
                newOrder.Tax = Math.Round(ConsoleIO.JustTax(newOrder.TaxRate, newOrder.Type, newOrder.Area, newOrder.State), 2, MidpointRounding.AwayFromZero);
                newOrder.TotalCost = Math.Round(ConsoleIO.GetTotal(newOrder.TaxRate, newOrder.Type, newOrder.Area, newOrder.State), 2, MidpointRounding.AwayFromZero);

                string confirmFormat = "{0, -16} {1} \n{2, -16} {3} \n{4, -16} {5:d} \n{6, -16} {7} \n{8, -16} {9} \n \n{10, -16} {11} \n{12, -16} {13} \n{14, -16} {15} \n{16, -16} {17}";

                ConsoleIO.Header();

                while (true)
                {
                    Console.WriteLine(confirmFormat, "Customer Name:", newOrder.Name, "Customer State:", newOrder.State, "Order Date:", newOrder.OrderDate, "Flooring Type:", newOrder.Type, "Area:", newOrder.Area + "(sqft)", "Product Cost:", newOrder.CostMaterial, "Labor Cost:", newOrder.CostLabor, "Tax:", newOrder.Tax, "Total:", newOrder.TotalCost);
                    Console.WriteLine(ConsoleIO.Separator);
                    Console.WriteLine();
                    Console.WriteLine("Is this information correct? Y/N or Q to return to the main menu.");

                    string finishOrder = Console.ReadLine();

                    if ((finishOrder.ToUpper() == "Y") || (finishOrder.ToUpper() == "YES"))
                    {
                        OrderManager orderManager = OrderManagerFactory.Create();
                        OrderAddResponse response = orderManager.AddOrder(newOrder);

                        ConsoleIO.Header();
                        Console.WriteLine("Thank you for your order.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        orderComplete = true;
                        break;
                    }
                    else if ((finishOrder.ToUpper() == "N") || (finishOrder.ToUpper() == "NO"))
                    {
                        orderComplete = false;
                        break;
                    }
                    else if ((finishOrder.ToUpper() == "Q") || (finishOrder.ToUpper() == "QUIT"))
                    {
                        return;
                    }
                    else
                    {
                        orderComplete = false;
                        ConsoleIO.Header();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter Y/N only. Or enter Q to return to the main menu.");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }
            }
            while (!orderComplete);
        }        
    }
}