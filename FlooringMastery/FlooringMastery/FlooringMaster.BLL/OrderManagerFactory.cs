using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using FlooringMastery.Data;

namespace FlooringMaster.BLL
{
    public static class OrderManagerFactory
    {
        public static OrderManager Create()
        {
            string mode = ConfigurationManager.AppSettings["Mode"].ToString();

            switch (mode)
            {
                case "MockRepo":
                    return new OrderManager(new MockOrderRepository());
                case "FileOrderRepo":
                    return new OrderManager(new FileOrderRepository());
                default:
                    throw new Exception("Mode value in app config is not valid");
            }
        }
    }
}