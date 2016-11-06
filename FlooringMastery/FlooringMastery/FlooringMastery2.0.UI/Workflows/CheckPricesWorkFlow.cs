using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery2._0.UI.Workflows
{
    class CheckPricesWorkFlow
    {
        public void Execute()
        {
            ConsoleIO.Header();
            Console.WriteLine("Current Prices (Not Including Sales Tax)");
            Console.WriteLine();
            string lineFormat = "{0, -15} {1, -9} {2, -9} {3,-9} {4, -9}";
            Console.WriteLine(lineFormat, string.Empty, "Carpet", "Laminate", "Tile", "Wood");
            Console.WriteLine(lineFormat, "Material(Sqft):", "2.25$", "1.75$", "3.50$", "5.15$");
            Console.WriteLine(lineFormat, "Labor(Sqft):", "2.10$", "2.10$", "4.15$", "4.75$");
            Console.WriteLine(lineFormat, "Total(Sqft)", "4.35$", "3.85$", "7.65$", "9.90$");
            Console.WriteLine();
            Console.WriteLine(ConsoleIO.Separator);
            Console.WriteLine();
            Console.WriteLine("Press any key to return to the main menu.");
            Console.ReadKey();
        }
    }
}