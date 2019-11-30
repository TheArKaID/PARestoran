using System;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Projek_Restoran.Models;

[assembly: HostingStartup(typeof(Projek_Restoran.Areas.Identity.IdentityHostingStartup))]
namespace Projek_Restoran.Areas.Identity
{
    public class IdentityHostingStartup : IHostingStartup
    {
        public void Configure(IWebHostBuilder builder)
        {
            builder.ConfigureServices((context, services) => {
                services.AddDbContext<WEB_ProjekAkhirContext>(options =>
                    options.UseSqlServer(
                        context.Configuration.GetConnectionString("DevConnection")));

                services.AddDefaultIdentity<IdentityUser>()
                    .AddEntityFrameworkStores<WEB_ProjekAkhirContext>();
            });
        }
    }
}