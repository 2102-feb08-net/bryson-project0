using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public class CustomerRepository : BaseRepository
    {
        public CustomerRepository(string connectionString, Action<string> logger) : base(connectionString, logger) { }
    }
}
