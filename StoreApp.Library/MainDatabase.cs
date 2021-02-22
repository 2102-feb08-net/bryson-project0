using StoreApp.DataAccess.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class MainDatabase
    {
        public IProductRepository ProductRepository { get; set; }

        public IOrderRepository OrderRepository { get; set; }

        public ILocationRepository LocationRepository { get; set; }

        public ICustomerRepository CustomerRepository { get; set; }
    }
}