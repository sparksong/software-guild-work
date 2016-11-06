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
    public class RemoveOrderWorkflow
    {
        public void Execute()
        {
            bool removed;
            int orderNumber;

            ConsoleIO.Header();
            Console.WriteLine("Remove an Order");
            Console.WriteLine();
            do
            {
                DateTime removeDate = ConsoleIO.GetOrderDateFromUser("Please enter a date for the order you wish you remove: ");

                ConsoleIO.Header();
                Console.WriteLine("Remove an Order");
                Console.WriteLine();

                List<Order> orders = ConsoleIO.PrintOrderList(removeDate);

                Console.WriteLine(ConsoleIO.Separator);
                Console.WriteLine();
                if (orders == null)
                {
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    return;
                }

                Console.WriteLine("Please enter the order number you wish to remove: ");
                int.TryParse(Console.ReadLine(), out orderNumber);
                if(orderNumber <= 0)
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
                    else if ((i == orders.Count -1) && (orders[i].OrderNumber != orderNumber))
                    {
                        Console.WriteLine("Order Number {0} does not exist on {1}", orderNumber, removeDate);
                        Console.WriteLine("Press any key to continue");
                        Console.ReadKey();
                        return;
                    }
                }
                string removeOrder = ConsoleIO.GetYesNoAnswerFromUser($"Are you sure you want to remove order number {orderNumber} on {removeDate:d}? ");

                if ((removeOrder.ToUpper() == "Y") || (removeOrder.ToUpper() == "YES"))
                {
                    OrderManager orderManager = OrderManagerFactory.Create();
                    OrderRemoveResponse response = orderManager.RemoveOrder(removeDate, orderNumber);

                    ConsoleIO.Header();
                    Console.WriteLine("Order removed.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    removed = false;
                    break;
                }
                else if ((removeOrder.ToUpper() == "N") || (removeOrder.ToUpper() == "NO"))
                {
                    ConsoleIO.Header();
                    Console.WriteLine("Remove cancelled.");
                    Console.WriteLine("Press any key to continue.");
                    Console.ReadKey();
                    removed = false;
                    return;
                }
                else
                {
                    removed = true;
                    ConsoleIO.Header();
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Please enter Y/N only.");
                    Console.ResetColor();
                    Console.WriteLine();
                }

            }


            while (removed);
        }

    }
}