using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery2._0.UI.Workflows;

namespace FlooringMastery2._0.UI
{
    class MainMenu
    {
        public static void ShowMenu()
        {
            bool choice = true;
            while (choice)
            {
                MenuText();
                choice = MenuChoice();
            }
        }

        public static void MenuText()
        {
            ConsoleIO.Header();
            Console.WriteLine("Main Menu");
            Console.WriteLine();

            Console.WriteLine("1) Display Orders");
            Console.WriteLine("2) Add Order");
            Console.WriteLine("3) Edit Order");
            Console.WriteLine("4) Remove Order");
            Console.WriteLine("5) Check Prices");
            Console.WriteLine();
            Console.WriteLine("Q) Quit");
            Console.WriteLine();
            Console.WriteLine(ConsoleIO.Separator);
            Console.WriteLine();
            Console.Write("Enter a choice: ");
        }

        private static bool MenuChoice()
        {
            string userInput = Console.ReadLine();

            switch (userInput.ToUpper())
            {
                case "1":
                    DisplayOrdersWorkflow displayOrders = new DisplayOrdersWorkflow();
                    displayOrders.Execute();
                    break;
                case "2":
                    AddOrderWorkflow addOrder = new AddOrderWorkflow();
                    addOrder.Execute();
                    break;
                case "3":
                    EditOrderWorkflow editOrder = new EditOrderWorkflow();
                    editOrder.Execute();
                    break;
                case "4":
                    RemoveOrderWorkflow removeOrder = new RemoveOrderWorkflow();
                    removeOrder.Execute();
                    break;
                case "5":
                    CheckPricesWorkFlow checkPrices = new CheckPricesWorkFlow();
                    checkPrices.Execute();
                    break;
                case "Q":
                    return false;
                default:
                    Console.WriteLine("Please enter a valid choice. Press any key to continue.");
                    Console.ReadLine();
                    break;
            }

            return true;
        }
    }
}