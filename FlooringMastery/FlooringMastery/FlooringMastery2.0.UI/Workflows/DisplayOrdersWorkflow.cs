using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using FlooringMastery.Data;
using FlooringMastery.Models;
using FlooringMaster.BLL;
using FlooringMastery.Models.Responses;

namespace FlooringMastery2._0.UI.Workflows
{
    public class DisplayOrdersWorkflow
    {
        public void Execute()
        {
            ConsoleIO.Header();
            Console.WriteLine("List Orders");
            Console.WriteLine();

            DateTime orderDate = ConsoleIO.GetOrderDateFromUser("Please enter a date for which you would like to list the orders. (MM/DD/YYYY): ");

            ConsoleIO.PrintOrderList(orderDate);
            
            Console.WriteLine();
            Console.WriteLine(ConsoleIO.Separator);
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}