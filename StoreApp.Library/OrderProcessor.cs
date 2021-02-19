using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.Library
{
    public class OrderProcessor
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="order"></param>
        /// <returns>Whether it was succesful in processing the order.</returns>
        public bool TryProcessOrder(IOrder order)
        {
            return false;
        }
    }
}
