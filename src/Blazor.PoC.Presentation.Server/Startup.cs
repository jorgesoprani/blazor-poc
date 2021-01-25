using Blazor.PoC.Application;
using Blazor.PoC.Infrastructure;
using Blazor.PoC.Presentation.Server.Services;
using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.Server {
    public class Startup {
        public Startup(IConfiguration configuration) {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services) {
            services.AddRazorPages();
            services.AddServerSideBlazor();

            services.AddInfrastructure(Configuration);
            services.AddApplication();

            services.AddBlazoredModal();
            services.AddBlazorToastr();
            services.AddBlazorise(options => {
                options.ChangeTextOnKeyPress = true;
            })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            services.AddTelerikBlazor();
            services.AddSyncfusionBlazor();

            services.AddTransient(x =>
                x.GetService<IConfiguration>().GetSection("AppSetting").Get<TestSettings>()
            );

            var test = Configuration.GetConnectionString("Blazor");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env) {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            } else {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.ApplicationServices
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            app.UseEndpoints(endpoints => {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }

    public class TestSettings {
        public string Test { get; set; }
    }
}
