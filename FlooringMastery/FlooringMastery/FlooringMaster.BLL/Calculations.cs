using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMaster.BLL
{
    public class Calculations
    {
        public static decimal CalculateCost(ProductType Type, int areaSqFt)
        {
            decimal productCost = GetProductCost(Type, areaSqFt);
            decimal laborCost = GetLaborCost(Type, areaSqFt);
            decimal costBeforeTax;
            costBeforeTax = productCost + laborCost;

            return costBeforeTax;
        }

        public static decimal GetProductCost(ProductType Type, int areaSqFt)
        {
            decimal productCost;

            switch (Type)
            {
                case ProductType.Carpet:
                    return productCost = 2.25M * areaSqFt;

                case ProductType.Laminate:
                    return productCost = 1.75M * areaSqFt;

                case ProductType.Tile:
                    return productCost = 3.50M * areaSqFt;

                case ProductType.Wood:
                    return productCost = 5.15M * areaSqFt;

                default:
                    throw new Exception($"Error reading ProductType.");
            }
        }

        public static decimal GetLaborCost(ProductType Type, int areaSqFt)
        {
            decimal laborCost;

            switch (Type)
            {
                case ProductType.Carpet:
                    return laborCost = 2.10M * areaSqFt;

                case ProductType.Laminate:
                    return laborCost = 2.10M * areaSqFt;

                case ProductType.Tile:
                    return laborCost = 4.15M * areaSqFt;

                case ProductType.Wood:
                    return laborCost = 4.75M * areaSqFt;
                default:
                    throw new Exception($"Error reading ProductType.");
            }
        }
    }
}