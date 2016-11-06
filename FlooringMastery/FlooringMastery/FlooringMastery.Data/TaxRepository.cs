using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data
{
    public class TaxRepository
    {
        public const string TAXFILE = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\bin\Debug\Orders\Taxes.txt";

        public static List<Tax> List()
        {

            List<Tax> taxes = new List<Tax>();

            using (StreamReader sr = new StreamReader(TAXFILE))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    Tax stateTax = new Tax();

                    string[] columns = line.Split('|');
                    string stateTaxRate = columns[0];
                    stateTax.TaxRate = decimal.Parse(columns[2]);

                    taxes.Add(stateTax);
                }
            }

            return taxes;
        }

        public static decimal GetTaxRate(string state)
        {
            var taxes = List();

            using (StreamReader sr = new StreamReader(TAXFILE))
            {
                sr.ReadLine();
                string line;
                while ((line = sr.ReadLine()) != null)
                {
                    string[] columns = line.Split('|');
                    string stateTax = columns[0].ToString();
                    decimal stateTaxRate = decimal.Parse(columns[2]);

                    if (state.ToUpper() == stateTax)
                    {
                        return stateTaxRate / 100;
                    }
                }
                return -1;
            }
        }
    }
}