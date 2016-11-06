using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FlooringMastery.Models.Interfaces;
using FlooringMastery.Models;
using FlooringMastery.Models.Responses;
using System.IO;

namespace FlooringMaster.BLL
{
    public class OrderManager
    {
        public static string ERROR_PATH = @"C:\Users\apprentice\Documents\Repositories\justin-frederiksen-individual-work\FlooringMastery\FlooringMastery\FlooringMastery.Data\bin\Debug\Orders\~/Error/" + DateTime.Today.ToString("MM-dd-yyyy") + ".txt";

        private IOrderRepository _orderRepository;

        public OrderManager(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public OrderLookupResponse LookupOrder(DateTime orderDate)
        {

            OrderLookupResponse response = new OrderLookupResponse();
            List<Order> orders = null;
            try
            {
                orders = _orderRepository.List(orderDate);

            }
            catch (IOException ex)
            {
                using (StreamWriter writer = new StreamWriter(ERROR_PATH, true))
                {
                    writer.WriteLine("Message :" + ex.Message + "<br/>" + Environment.NewLine + "StackTrace :" + ex.StackTrace +
                       "" + Environment.NewLine + "Date :" + DateTime.Now.ToString());
                    writer.WriteLine(Environment.NewLine + "-----------------------------------------------------------------------------" + Environment.NewLine);
                }
                response.Success = false;
                response.Message = ex.Message;
                return response;
            }

            response.orders = orders;
            if (response.orders == null)
            {
                response.Success = false;
                response.Message = $"/n{orderDate} is not a valid date.";
            }
            else
            {
                response.Success = true;
            }

            return response;
        }

        public OrderAddResponse AddOrder(Order newOrder)
        {
            OrderAddResponse response = new OrderAddResponse();

            Validation(newOrder, response);
            if (response.Success == false)
            {
                return response;
            }
            _orderRepository.Add(newOrder);
            response.newOrder = newOrder;

            if (response.newOrder.OrderNumber > 0)
            {
                response.Success = true;
            }
            else
            {
                response.Success = false;
            }

            return response;
        }

        public OrderRemoveResponse RemoveOrder(DateTime orderDate, int orderNumber)
        {
            OrderRemoveResponse response = new OrderRemoveResponse();
            List<Order> orders = _orderRepository.List(orderDate);

            if (orderDate < new DateTime(2016, 1, 1))
            {
                response.Success = false;
                response.Message = "No orders before the current year may be deleted.";
                return response;
            }
            
            if (orders == null || orders.Count == 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                foreach (var order in orders)
                {
                    if (orderNumber != order.OrderNumber)
                    {
                        continue;
                    }
                    else
                    {
                        _orderRepository.Delete(orderDate, orderNumber);
                        response.Success = true;
                        response.Message = "Order deleted";
                        return response;
                    }
                }
                response.Success = false;
                return response;
            }
        }

        public OrderEditResponse EditOrder(Order editedOrder, int orderNumber)
        {
            OrderEditResponse response = new OrderEditResponse();

            Validation(editedOrder, response);
            if (response.Success == false)
            {
                return response;
            }

            List<Order> orders = _orderRepository.List(editedOrder.OrderDate);
            if (orders == null || orders.Count == 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                foreach (var order in orders)
                {
                    if (orderNumber != order.OrderNumber)
                    {
                        continue;
                    }
                    else
                    {
                        _orderRepository.Edit(editedOrder, orderNumber);
                        response.Success = true;
                        response.Message = "Order Edited";
                        return response;
                    }
                }
                response.Success = false;
                return response;
            }
        }

        public Response Validation(Order newOrder, Response response)
        {
            if (string.IsNullOrWhiteSpace(newOrder.Name) || newOrder.Name.Length > 20)
            {
                response.Success = false;
                return response;
            }
            if (newOrder.OrderDate < new DateTime(2016, 1, 1))
            {
                response.Success = false;
                response.Message = "No orders may be placed before the current year.";
                return response;
            }
            if (string.IsNullOrWhiteSpace(newOrder.State) || newOrder.State.Length > 2)
            {
                response.Success = false;
                return response;
            }
            if (newOrder.Area <= 0)
            {
                response.Success = false;
                return response;
            }
            else
            {
                response.Success = true;
                return response;
            }
        }
    }
}