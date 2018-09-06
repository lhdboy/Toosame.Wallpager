using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Toosame.Wallpager.Data.SqlServer.Service;

namespace Toosame.Wallpager
{
    public class Program
    {
        public static void Main(string[] args)
        {
            CreateWebHostBuilder(args).Build().Run();
        }

        public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseSqlServer(@"Data Source=(localdb)\ProjectsV13;Initial Catalog=Wallpager;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False")
                .UseStartup<Startup>();
    }
}
