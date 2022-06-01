using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Localization;
using Microsoft.JSInterop;

namespace BlazorServerLocalizationTEST.Shared
{
    public partial class MainLayout
    {
        private string English = "en";
        private string Italian = "it";
        private string Ukranian = "uk";

        private string text = string.Empty;

        CancellationTokenSource cancellationToken = new CancellationTokenSource();

        protected override async Task OnInitializedAsync()
        {
            await ourService.StartAsync(cancellationToken.Token);
            ourService.RaisedAlarm += ourService_RaisedAlarm;
        }

        private async Task OnSelected(string language)
        {
            var name = CookieRequestCultureProvider.DefaultCookieName;
            NavManager.NavigateTo(@$"Culture/{language}", true);
            await ProtectedSessionStore.SetAsync(name, language);
        }

        private async void ourService_RaisedAlarm(object? sender, string e)
        {
            // DARIO
            //return;

            try
            {
                string currentCulture = System.Threading.Thread.CurrentThread.CurrentCulture.Name;
                if (currentCulture.Length > 0)
                {
                    CultureInfo.DefaultThreadCurrentCulture = new CultureInfo(currentCulture);
                    CultureInfo.DefaultThreadCurrentUICulture = new CultureInfo(currentCulture);
                }

                var t = localizer[e].Value;
                if (t.Length > 0)
                {
                    text = t;
                    await InvokeAsync(() => StateHasChanged());
                }
            }
            catch (JSDisconnectedException jsDisconnectedException)
            {
                Console.WriteLine(jsDisconnectedException);
            }
        }
    }
}
