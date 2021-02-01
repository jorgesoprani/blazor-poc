using Blazor.PoC.Presentation.WebAssembly.Api;
using Blazor.PoC.Presentation.WebAssembly.Services;
using Blazored.Modal;
using Blazorise;
using Blazorise.Bootstrap;
using Blazorise.Icons.FontAwesome;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Syncfusion.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly {
    public class Program {
        public static async Task Main(string[] args) {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://localhost:44342") });
            //builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri("https://blazor-tests-api.azurewebsites.net/") });
            builder.Services.AddScoped<ApiService>();

            builder.Services.AddBlazoredModal();
            builder.Services.AddBlazorToastr();
            builder.Services
                .AddBlazorise(options => {
                    options.ChangeTextOnKeyPress = true;
                })
                .AddBootstrapProviders()
                .AddFontAwesomeIcons();

            builder.Services.AddSyncfusionBlazor();

            var host = builder.Build();

            host.Services
              .UseBootstrapProviders()
              .UseFontAwesomeIcons();

            await host.RunAsync();
        }
    }
}
