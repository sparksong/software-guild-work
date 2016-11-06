using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace FlooringMastery.Models
{
    public class Order
    {
        public int OrderNumber { get; set; }

        public string Name { get; set; }

        public DateTime OrderDate { get; set; }

        public string State { get; set; }

        public ProductType Type { get; set; }

        public int Area { get; set; }

        public decimal CostLabor { get; set; }

        public decimal CostMaterial { get; set; }

        public decimal TaxRate { get; set; }

        public decimal Tax { get; set; }

        public decimal TotalCost { get; set; }
    }
}