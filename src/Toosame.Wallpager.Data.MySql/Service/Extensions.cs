using Microsoft.Extensions.DependencyInjection;

namespace Toosame.Wallpager.Data.MySql.Service
{
    public static class Extensions
    {
        public static void AddMySqlDataSource(this IServiceCollection service)
        {
            service.AddTransient<IDataSourceService, MySqlDataSourceService>();
        }
    }
}
