using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Localization;
using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorServerLocalizationTEST.Services;
public class GenericService : IHostedService, IDisposable
{
    public event EventHandler<string>? RaisedAlarm;
    private int counter;
    private CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
    private Timer timer = null!;

    public GenericService()
    {

    }
    private void Run(object? state)
    {
        RaisedAlarm?.Invoke(this, (++counter).ToString("000"));
        if (counter >= 5)
        {
            counter = 0;
        }
    }

    public Task StartAsync(CancellationToken cancellationToken)
    {
        timer = new Timer(Run, null, TimeSpan.Zero, TimeSpan.FromSeconds(5));
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        timer?.Change(Timeout.Infinite, 0);
        return Task.CompletedTask;
    }

    public void Dispose()
    {
        timer?.Dispose();
    }
}

