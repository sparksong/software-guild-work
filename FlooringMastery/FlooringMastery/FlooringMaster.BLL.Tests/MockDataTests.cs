using FlooringMastery.Models;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models.Responses;
using FlooringMastery2._0.UI.Workflows;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMaster.BLL.Tests
{


    [TestFixture]
    public class MockDataTests
    {
        [Test]
        [TestCase("", false)]                   //Empty string for date. Fail.
        [TestCase(" ", false)]                  //Whitespace for date. Fail.
        [TestCase("-10/16/2016", false)]        //Negative numbers for date. Fail.
        [TestCase("133299999", false)]          //Invalid input for date. Fail.
        [TestCase("asdfsdfsdfsdfsdf", false)]   //Letters for date. Fail.
        [TestCase("11/22/2017", false)]         //Date after today's date. Fail.
        [TestCase("10/16/2016", true)]          //Correct date. Pass.

        public void ListOrderTest(string orderDateString, bool expectedResult)
        {
            DateTime orderDate;
            DateTime.TryParse(orderDateString, out orderDate);

            OrderManager lookupTester = OrderManagerFactory.Create();

            OrderLookupResponse response = lookupTester.LookupOrder(orderDate);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [Test]
        [TestCase("", "OH", "10/16/2016", ProductType.Carpet, 100, false)]                                                       //Empty string for name. Fail.
        [TestCase("Sir Edward Cumberton", "", "10/16/2016", ProductType.Carpet, 100, false)]                                     //Empty string for state. Fail.
        [TestCase("Sir Edward Cumberton", "OH", "10/16/2016", ProductType.Carpet, -100, false)]                                  //Negative area. Fail.
        [TestCase("Sir Edward Cumberton", "OH", "10/16/1016", ProductType.Carpet, 0, false)]                                     //Area = 0. Fail.
        [TestCase("OHNOESMYNAMEISWAYTOOLONGSOMEONEPLEASESTOPMENOWORELSE", "OH", "10/16/2016", ProductType.Carpet, 100, false)]   //Name too long. Fail.
        [TestCase("Sir Edward Cumberton", "OH", "10/16/2016", ProductType.Carpet, 100, true)]                                    //Correct. Pass.

        public void AddOrderTest(string name, string state, string orderDateString, ProductType type, int area, bool expectedResult)
        {
            Order newOrder = new Order();

            DateTime orderDate;
            DateTime.TryParse(orderDateString, out orderDate);

            newOrder.Name = name;
            newOrder.State = state;
            newOrder.OrderDate = orderDate;
            newOrder.Type = ProductType.Carpet;
            newOrder.Area = area;

            OrderManager addTester = OrderManagerFactory.Create();

            OrderAddResponse response = addTester.AddOrder(newOrder);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [Test]
        [TestCase("10/16/2016", 10, false)]  //Date correct, orderNumber not. Fail.
        [TestCase("10/1/2016", 2, false)]    //Number correct, date not. Fail.
        [TestCase("11/11/2017", 10, false)]  //Both date and number incorrect. Fail.
        [TestCase("10/16/2016", 2, true)]    //Both date and number correct. Pass.

        public void DeleteOrderTest(string orderDateString, int orderNumber, bool expectedResult)
        {
            OrderManager deleteTester = OrderManagerFactory.Create();

            DateTime orderDate;
            DateTime.TryParse(orderDateString, out orderDate);

            OrderRemoveResponse response = deleteTester.RemoveOrder(orderDate, orderNumber);
            Assert.AreEqual(expectedResult, response.Success);
        }

        [Test]
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "MN", 0, false)] //Area 0. False.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "MN", -100, false)] //Negative area. False.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "", 100, false)] //Empty state. False.
        [TestCase("10/16/2016", 1, "", "MN", 100, false)] //Empty name. False.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II who hails from the land beyond the other land.", "MN", 100, false)] //Name too long. False.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "MN", 100, true)] //Change nothing. True.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "MN", 100, true)] //Change nothing. True.
        [TestCase("10/16/2016", 1, "Steve Johanson", "MN", 100, true)] //Change name only. True.
        [TestCase("10/16/2016", 1, "Steve Johanson", "OH", 100, true)] //Change State only. True.
        [TestCase("10/16/2016", 1, "Steve Johanson", "MN", 456, true)] //Change area only. True.
        [TestCase("10/16/2016", 1, "Lord Edgesworth II", "OH", 456, true)] //Change everything. True.

        public void EditOrderTest(string orderDateString, int orderNumber, string name, string state, int area, bool expectedResult)
        {
            OrderManager editTester = OrderManagerFactory.Create();
            DateTime orderDate;
            DateTime.TryParse(orderDateString, out orderDate);

            Order editedOrder = new Order();
            editedOrder.Name = name;
            editedOrder.OrderNumber = orderNumber;
            editedOrder.State = state;
            editedOrder.Area = area;
            editedOrder.OrderDate = orderDate;

            OrderEditResponse response = editTester.EditOrder(editedOrder, orderNumber);
            Assert.AreEqual(expectedResult, response.Success);
        }
    }
}
