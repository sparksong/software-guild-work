using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;

namespace FlooringMastery.Data
{
    public class FileOrderRepository : IOrderRepository
    {
        private const string _DATE_FORMAT = "MM/dd/yyyy";
        private const string _RW_DATE_FORMAT = "MMddyyyy";
        private string _filePath;
        public const string pathTemplate = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\bin\Debug\Orders\Orders_{0:MMddyyyy}.txt";

        public FileOrderRepository()
        {
            _filePath = pathTemplate;
        }

        public List<Order> List(DateTime orderDate)
        {
            List<Order> orders = new List<Order>();

            var effectivePath = string.Format(_filePath, orderDate);

            if (File.Exists(effectivePath))
            {
                using (StreamReader sr = new StreamReader(effectivePath))
                {
                    sr.ReadLine();
                    string line;
                    while ((line = sr.ReadLine()) != null)
                    {
                        Order newOrder = new Order();

                        string[] columns = line.Split('|');
                        newOrder.OrderNumber = int.Parse(columns[0]);
                        newOrder.Name = columns[1];
                        newOrder.State = columns[2];
                        newOrder.OrderDate = DateTime.ParseExact(columns[3], _DATE_FORMAT, CultureInfo.InvariantCulture);
                        newOrder.Area = int.Parse(columns[4]);
                        newOrder.Type = (ProductType)Enum.Parse(typeof(ProductType), columns[5]);
                        newOrder.CostMaterial = Math.Round(decimal.Parse(columns[6]), 2, MidpointRounding.AwayFromZero);
                        newOrder.CostLabor = Math.Round(decimal.Parse(columns[7]), 2, MidpointRounding.AwayFromZero);
                        newOrder.Tax = Math.Round(decimal.Parse(columns[8]), 2, MidpointRounding.AwayFromZero);
                        newOrder.TotalCost = Math.Round(decimal.Parse(columns[9]), 2, MidpointRounding.AwayFromZero);

                        orders.Add(newOrder);
                    }
                }

                return orders;
            }
            else
            {
                return null;
            }
        }

        public void Add(Order order)
        {
            DateTime orderDate = order.OrderDate;
            var effectivePath = string.Format(_filePath, orderDate);
            List<Order> orders = List(orderDate);

            if (orders == null || orders.Count == 0)
            {
                order.OrderNumber = 1;
            }

            else
            {
                order.OrderNumber = orders.Max(number => number.OrderNumber) + 1;
            }
            CreateOrderFile(orders, order.OrderDate);
            using (StreamWriter sw = new StreamWriter(effectivePath, true))
            {
                string line = CreateCsvForOrder(order);

                sw.WriteLine(line);
            }
        }

        public void Edit(Order order, int orderNumber)
        {
            var orders = List(order.OrderDate);

            orders[orderNumber -1] = order;

            CreateOrderFile(orders, order.OrderDate);
        }

        public void Delete(DateTime orderDate, int orderNumber)
        {
            var effectivePath = string.Format(_filePath, orderDate);

            if (File.Exists(effectivePath))
            {
                var orders = List(orderDate);
                orders.RemoveAt(orderNumber - 1);

                CreateOrderFile(orders, orderDate);
            }
        }

        private string CreateCsvForOrder(Order order)
        {
            return string.Format("{0}|{1}|{2}|{3:" + _DATE_FORMAT + "}|{4}|{5}|{6}|{7}|{8}|{9}", order.OrderNumber, order.Name, order.State, order.OrderDate, order.Area, order.Type, order.CostMaterial, order.CostLabor, order.Tax, order.TotalCost);
        }

        private void CreateOrderFile(List<Order> orders, DateTime orderDate)
        {
            var effectivePath = string.Format(_filePath, orderDate);

            using (StreamWriter sw = new StreamWriter(effectivePath))
            {
                sw.WriteLine("OrderNumber|Name|State|OrderDate|Area|Type|MaterialCost|LaborCost|Tax|Total");
                if (orders != null)
                {
                    foreach (var order in orders)
                    {
                        sw.WriteLine(CreateCsvForOrder(order));
                    }
                }
            }
        }
    }
}