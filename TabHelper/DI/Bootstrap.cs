using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using TabHelper.Data.ORM;
using TabHelper.Data.Persistence;
using TabHelper.Data.Persistence.Interfaces;
using TabHelper.Data.Transaction;

namespace TabHelper.DI
{
    public static class Bootstrap
    {
        public static void Configure(IServiceCollection services, string conn)
        {
            services.AddDbContext<AppDbContext>(options => options.UseMySql(conn, 
                builder => builder.MigrationsAssembly("TabHelper")));

            services.AddScoped(typeof(IUnitOfWork), typeof(UnitOfWork));

            // # repository base {entities}
            /// <summary>
            /// use this manipule internal entities from db source
            /// </summary>
            services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        }
    }
}
