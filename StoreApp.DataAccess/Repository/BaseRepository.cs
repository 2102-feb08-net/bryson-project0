using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace StoreApp.DataAccess.Repository
{
    public abstract class BaseRepository
    {
        protected DbContextOptions<DigitalStoreContext> Options { get; private set; }

        protected Action<string> Logger { get; private set; }

        public BaseRepository(string connectionString, Action<string> logger)
        {
            Options = CreateOptions<DigitalStoreContext>(connectionString, logger);
            Logger = logger;
        }

        protected static DbContextOptions<T> CreateOptions<T>(string connectionString, Action<string> logger = null) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>()
                .UseSqlServer(connectionString);

            if (logger is not null)
                optionsBuilder.LogTo(logger, Microsoft.Extensions.Logging.LogLevel.Information);

            return optionsBuilder.Options;
        }
    }
}