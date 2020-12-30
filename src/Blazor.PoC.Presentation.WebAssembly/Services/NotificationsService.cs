using Microsoft.Extensions.DependencyInjection;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Blazor.PoC.Presentation.WebAssembly.Services
{
    public class NotificationsService
    {
        private IJSRuntime _jsRuntime;
        public NotificationsService(IJSRuntime jSRuntime)
        {
            _jsRuntime = jSRuntime;
        }

        public async Task ShowInfo(string message, string title = null)
        {
            var options = new
            {
                CloseButton = true,
                ShowDuration = 300,
                HideDuration = 300,
                TimeOut = 5000,
                ProgressBar = true,
                HideMethod = "slideUp",
                ShowMethod = "slideDown",
                PositionClass = "toast-bottom-right"
            };

            await _jsRuntime.InvokeVoidAsync("toastrFunctions.info", message, title, options);
        }

        public async Task ShowWarning(string message, string title = null)
        {
            var options = new
            {
                CloseButton = true,
                ShowDuration = 300,
                HideDuration = 300,
                TimeOut = 5000,
                ProgressBar = true,
                HideMethod = "slideUp",
                ShowMethod = "slideDown",
                PositionClass = "toast-bottom-right"
            };

            await _jsRuntime.InvokeVoidAsync("toastrFunctions.warning", message, title, options);
        }

        public async Task ShowError(string message, string title = null)
        {
            var options = new
            {
                CloseButton = true,
                ShowDuration = 300,
                HideDuration = 300,
                TimeOut = 5000,
                ProgressBar = true,
                HideMethod = "slideUp",
                ShowMethod = "slideDown",
                PositionClass = "toast-bottom-right"
            };

            await _jsRuntime.InvokeVoidAsync("toastrFunctions.error", message, title, options);
        }
    }
    public static class ServiceCollectionExtension
    {
        public static IServiceCollection AddBlazorToastr(this IServiceCollection services)
            => services.AddScoped<NotificationsService>();
    }
}
