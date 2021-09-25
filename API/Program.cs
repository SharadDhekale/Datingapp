using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;

namespace API
{
    public class Program
    {
        public async static Task Main(string[] args)
        {
            var host= CreateHostBuilder(args).Build();
            using var scop= host.Services.CreateScope();
            var service= scop.ServiceProvider;
            try
            {
                var dbCcontext =service.GetRequiredService<DataContext>();
                
                await dbCcontext.Database.MigrateAsync();
                var result= await Seed.SeedUsers(dbCcontext);
            }
            catch (Exception ex)
            {
                var logger=service.GetRequiredService<ILogger>();
                logger.LogError(ex,"Error Occured while doing Data migration");
                throw;
            }
            await host.RunAsync();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureWebHostDefaults(webBuilder =>
                {
                    webBuilder.UseStartup<Startup>();
                });
    }
}
