using FlooringMaster.BLL;
using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery2._0.UI.Workflows
{
    public class EditOrderWorkflow
    {
        public void Execute()
        {
            bool edited;
            int orderNumber;

            ConsoleIO.Header();
            Console.WriteLine("Edit an Order");
            Console.WriteLine();
            do
            {
                DateTime editDate = ConsoleIO.GetOrderDateFromUser("Please enter a date for the order you wish you edit (MM/DD/YYYY): ");

                ConsoleIO.Header();
                Console.WriteLine("Edit an Order");
                Console.WriteLine();

                List<Order> orders = ConsoleIO.PrintOrderList(editDate);

                Console.WriteLine(ConsoleIO.Separator);
                Console.WriteLine();
                if (orders == null)
                {
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Please enter the order number you wish to edit: ");
                int.TryParse(Console.ReadLine(), out orderNumber);
                if (orderNumber <= 0)
                {
                    Console.WriteLine("Order Number is not valid.");
                    Console.WriteLine("Press any key to continue");
                    Console.ReadKey();
                    return;
                }
                for (int i = 0; i < orders.Count; i++)
                {
                    if (orderNumber == orders[i].OrderNumber)
                    {
                        break;
                    }
                    else if ((i == orders.Count - 1) && (orders[i].OrderNumber != orderNumber))
                    {
                        Console.WriteLine("Order Number {0} does not exist on {1}", orderNumber, editDate);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        return;
                    }
                }

                Order oldOrder = orders[orderNumber - 1];

                oldOrder.Name = ConsoleIO.GetOrderNameFromUser(String.Format("Please enter name for the order. Current name : ({0}) ", oldOrder.Name));
                oldOrder.State = ConsoleIO.GetOrderStateFromUser(String.Format("Please enter your state initials. Current state: ({0}) ", oldOrder.State));
                oldOrder.TaxRate = TaxRepository.GetTaxRate(oldOrder.State);
                oldOrder.OrderDate = editDate;
                oldOrder.Type = ConsoleIO.GetProductFromUser(oldOrder.Type);
                oldOrder.Area = ConsoleIO.GetAreaFromUser(String.Format("Please enter new area amount. Current Area: ({0}) ", oldOrder.Area));

                oldOrder.CostLabor = Math.Round(Calculations.GetLaborCost(oldOrder.Type, oldOrder.Area), 2, MidpointRounding.AwayFromZero);
                oldOrder.CostMaterial = Math.Round(Calculations.GetProductCost(oldOrder.Type, oldOrder.Area), 2, MidpointRounding.AwayFromZero);
                oldOrder.Tax = Math.Round(ConsoleIO.JustTax(oldOrder.TaxRate, oldOrder.Type, oldOrder.Area, oldOrder.State), 2, MidpointRounding.AwayFromZero);
                oldOrder.TotalCost = Math.Round(ConsoleIO.GetTotal(oldOrder.TaxRate, oldOrder.Type, oldOrder.Area, oldOrder.State), 2, MidpointRounding.AwayFromZero);


                string confirmFormat = "{0, -16} {1} \n{2, -16} {3} \n{4, -16} {5:d} \n{6, -16} {7} \n{8, -16} {9} \n \n{10, -16} {11} \n{12, -16} {13} \n{14, -16} {15} \n{16, -16} {17}";

                while (true)
                {
                    string editOrder = ConsoleIO.GetYesNoAnswerFromUser($"Is this information correct? ({oldOrder.Name}, {oldOrder.State}, {oldOrder.Type}, {oldOrder.Area})");

                    if ((editOrder.ToUpper() == "Y") || (editOrder.ToUpper() == "YES"))
                    {
                        ConsoleIO.Header();
                        OrderManager orderManager = OrderManagerFactory.Create();
                        OrderEditResponse response = orderManager.EditOrder(oldOrder, orderNumber);

                        Console.WriteLine(confirmFormat, "Customer Name:", oldOrder.Name, "Customer State:", oldOrder.State, "Order Date:", oldOrder.OrderDate, "Flooring Type:", oldOrder.Type, "Area:", oldOrder.Area + "(sqft)", "Product Cost:", oldOrder.CostMaterial, "Labor Cost:", oldOrder.CostLabor, "Tax:", oldOrder.Tax, "Total:", oldOrder.TotalCost);
                        Console.WriteLine(ConsoleIO.Separator);
                        Console.WriteLine();

                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        edited = true;
                        break;
                    }
                    else if ((editOrder.ToUpper() == "N") || (editOrder.ToUpper() == "NO"))
                    {
                        ConsoleIO.Header();
                        Console.WriteLine("Edit cancelled.");
                        Console.WriteLine("Press any key to continue.");
                        Console.ReadKey();
                        edited = false;
                        return;
                    }
                    else
                    {
                        edited = false;
                        ConsoleIO.Header();
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("Please enter Y/N only.");
                        Console.ResetColor();
                        Console.WriteLine();
                    }
                }

            }

            while (!edited);
        }
    }
}