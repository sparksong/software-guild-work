using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using FlooringMastery.Data;
using FlooringMastery.Models;
using System.IO;
using System.Globalization;

namespace FlooringMaster.BLL.Tests
{
    [TestFixture]
    public class FileOrderRepositoryTests
    {
        private const string _filePath = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\bin\Debug\Orders\Orders_10162016.txt";
        private const string _originalData = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\bin\Debug\Orders\OrderTestSeed.txt";

        [SetUp]
        public void Setup()
        {
            if (File.Exists(_filePath))
            {
                File.Delete(_filePath);
            }

            File.Copy(_originalData, _filePath);
        }

        [Test]
        public void CanReadDataFromFile()
        {
            DateTime orderDate = DateTime.Parse("10/16/2016");

            FileOrderRepository repo = new FileOrderRepository();

            List<Order> orders = repo.List(orderDate);

            Assert.AreEqual(4, orders.Count());

            Order check = orders[2];
            Assert.AreEqual(3, check.OrderNumber);
            Assert.AreEqual("Nate Ruess", check.Name);
            Assert.AreEqual("MN", check.State);
            Assert.AreEqual(new DateTime(2016, 10, 16), check.OrderDate);
            Assert.AreEqual(500, check.Area);
            Assert.AreEqual(1750.00, check.CostMaterial);
            Assert.AreEqual(2075.00, check.CostLabor);
            Assert.AreEqual(267.75, check.Tax);
            Assert.AreEqual(4092.75, check.TotalCost);
        }

        [Test]
        public void CanAddOrderToFile()
        {
            DateTime orderDate = DateTime.Parse("10/16/2016");
            FileOrderRepository repo = new FileOrderRepository();
            List<Order> orders = repo.List(orderDate);

            Order newOrder = new Order();
            newOrder.OrderNumber = 5;
            newOrder.Name = "Rickstar Fredders";
            newOrder.State = "MN";
            newOrder.OrderDate = orderDate;
            newOrder.Area = 222;
            newOrder.Type = ProductType.Tile;
            newOrder.CostMaterial = 777.00M;
            newOrder.CostLabor = 921.30M;
            repo.Add(newOrder);

            orders = repo.List(orderDate);

            Assert.AreEqual(5, orders.Count());

            Order check = orders[4];

            Assert.AreEqual(5, check.OrderNumber);
            Assert.AreEqual("Rickstar Fredders", check.Name);
            Assert.AreEqual("MN", check.State);
            Assert.AreEqual(new DateTime(2016, 10, 16), check.OrderDate);
            Assert.AreEqual(222, check.Area);
            Assert.AreEqual(ProductType.Tile, check.Type);
            Assert.AreEqual(777.00, check.CostMaterial);
            Assert.AreEqual(921.30, check.CostLabor);
        }

        [Test]
        public void CanDeleteOrder()
        {
            DateTime orderDate = DateTime.Parse("10/16/2016");
            FileOrderRepository repo = new FileOrderRepository();
            List<Order> orders = repo.List(orderDate);
            repo.Delete(orderDate, orders[0].OrderNumber);
            orders = repo.List(orderDate);          

            Assert.AreEqual(3, orders.Count);
            Order check = orders[0];

            Assert.AreEqual(2, check.OrderNumber);
            Assert.AreEqual("Steve Irwin", check.Name);
            Assert.AreEqual("MN", check.State);
            Assert.AreEqual(new DateTime(2016, 10, 16), check.OrderDate);
            Assert.AreEqual(216, check.Area);
            Assert.AreEqual(ProductType.Laminate, check.Type);
            Assert.AreEqual(378.00, check.CostMaterial);
            Assert.AreEqual(453.00, check.CostLabor);
            Assert.AreEqual(58.21, check.Tax);
            Assert.AreEqual(889.81, check.TotalCost);
        }

        [Test]
        public void CanEditOrder()
        {
            DateTime orderDate = DateTime.Parse("10/16/2016");
            FileOrderRepository repo = new FileOrderRepository();
            List<Order> orders = repo.List(orderDate);

            Order editedOrder = orders[0];
            editedOrder.Area = 7000;

            repo.Edit(editedOrder, 1);

            orders = repo.List(orderDate);

            Assert.AreEqual(4, orders.Count);

            Order check = orders[0];

            Assert.AreEqual(1, check.OrderNumber);
            Assert.AreEqual("James Camerson", check.Name);
            Assert.AreEqual("MN", check.State);
            Assert.AreEqual(new DateTime(2016, 10, 16), check.OrderDate);
            Assert.AreEqual(7000, check.Area);
            Assert.AreEqual(ProductType.Carpet, check.Type);
            Assert.AreEqual(112.50, check.CostMaterial);
            Assert.AreEqual(105.00, check.CostLabor);
            Assert.AreEqual(15.23, check.Tax);
            Assert.AreEqual(232.73, check.TotalCost);
        }
    }
}