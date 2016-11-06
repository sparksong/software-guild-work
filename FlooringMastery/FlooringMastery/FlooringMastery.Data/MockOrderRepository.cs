using FlooringMastery.Models.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models;

namespace FlooringMastery.Data
{
    public class MockOrderRepository : IOrderRepository
    {
        public static readonly List<Order> _orders = new List<Order>
        {
            new Order {Name = "Steve Johanson", OrderNumber = 1, OrderDate = DateTime.Parse("10/16/2016"), State = "MN", Area = 100, Type = ProductType.Carpet, CostMaterial = 112.50M, CostLabor = 105, Tax = 15.23M, TotalCost = 232.73M },

            new Order {Name = "Stan Leeeeee", OrderNumber = 2, OrderDate = DateTime.Parse("10/16/2016"), State = "MN", Area = 222, Type = ProductType.Tile, CostMaterial = 777.00M, CostLabor = 921.30M, Tax = 116.33M, TotalCost = 1814.63M },

            new Order {Name = "Bob Ross", OrderNumber = 3, OrderDate = DateTime.Parse("10/16/2016"), State = "OH", Area = 216, Type = ProductType.Laminate, CostMaterial = 378, CostLabor = 453, Tax = 58.21M, TotalCost = 889.81M}
        };

        public void Add(Order newOrder)
        {
            List<Order> orders = List(newOrder.OrderDate);

            if (orders == null || orders.Count == 0)
            {
                newOrder.OrderNumber = 1;
            }

            else
            {
                newOrder.OrderNumber = orders.Max(number => number.OrderNumber) + 1;
            }
            _orders.Add(newOrder);
        }

        public void Delete(DateTime orderDate, int orderNumber)
        {
            List<Order> orders = List(orderDate);
            _orders.RemoveAt(orderNumber - 1);
        }

        public void Edit(Order order, int orderNumber)
        {
            List<Order> orders = List(order.OrderDate);

            _orders[orderNumber - 1] = order;
        }

        public List<Order> List(DateTime orderDate)
        {
            Order newOrder = new Order();
            List<Order> orders = new List<Order>();

            foreach (var order in _orders)
                if (order.OrderDate == orderDate)
                {
                    newOrder = order;
                    orders.Add(newOrder);
                }
            if (orders.Count > 0)
            {
                return orders;
            }
            else
            {
                return null;
            }
        }
    }
}
