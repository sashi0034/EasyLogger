#nullable enable

using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;
using SimpleLogger.Main.Data;

namespace SimpleLogger.Main.Process;

public record PipeConnection(
    Dispatcher Dispatcher,
    LogTaker LogTaker)
{
    public async Task StartAsync(CancellationToken cancellation)
    {
#if DEBUG
        useDummies();
#else
        await process(cancellation);
#endif
    }

    private async Task process(CancellationToken cancellation)
    {
        LogTaker.Take("Waiting...");
        using StreamReader reader = new StreamReader(Console.OpenStandardInput(), Encoding.UTF8);
        while (cancellation.IsCancellationRequested == false)
        {
            var input = await reader.ReadLineAsync(cancellation);
            // addLogging("🗨️ input");
            if (input == null)
            {
                // addLogging("😢 null");
                await Task.Delay(1000, cancellation);
                continue;
            }

            Dispatcher.Invoke(() => { LogTaker.Take("📝 " + input); });
        }
    }


    private void useDummies()
    {
        foreach (var data in DummyTexts.Data)
        {
            LogTaker.Take(data);
        }
    }
}