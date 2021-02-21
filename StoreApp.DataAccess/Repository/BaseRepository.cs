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

        private const int STARTING_ID = 1;

        public BaseRepository(string connectionString, Action<string> logger)
        {
            Options = CreateOptions<DigitalStoreContext>(connectionString, logger);
            Logger = logger;
        }

        protected static DbContextOptions<T> CreateOptions<T>(string connectionString, Action<string> logger = null) where T : DbContext
        {
            var optionsBuilder = new DbContextOptionsBuilder<T>()
                .UseSqlServer(connectionString);

            if (logger != null)
                optionsBuilder.LogTo(logger, Microsoft.Extensions.Logging.LogLevel.Information);

            return optionsBuilder.Options;
        }

        protected static async Task<int> GenerateNextIdAsync<T>(IQueryable<T> query) where T : Library.Model.IIdentifiable
        {
            var lastObj = await query.OrderBy(q => q.Id).LastOrDefaultAsync();
            return lastObj != null ? lastObj.Id + 1 : STARTING_ID;
        }
    }
}