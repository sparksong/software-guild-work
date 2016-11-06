using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlooringMastery.Models.Interfaces
{
    public interface IOrderRepository
    {
        List<Order> List(DateTime orderDate);
        void Add(Order order);
        void Edit(Order order, int orderNumber);
        void Delete(DateTime orderDate, int orderNumber);
    }
}
