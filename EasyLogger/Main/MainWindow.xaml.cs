using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleLogger.Main.Control;
using SimpleLogger.Main.Process;
using SimpleLogger.Utils;

namespace SimpleLogger.Main
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CancellationTokenSource _cancellation = new();
        private readonly LogAddition _logAddition;

        public MainWindow()
        {
            InitializeComponent();

            _logAddition = new LogAddition(stackPanel);

#if DEBUG
            useTestTexts();
#else
            connectToPipe(_cancellation.Token).RunErrorHandler();
#endif
        }

        private void useTestTexts()
        {
            foreach (var data in TestTexts.Data)
            {
                _logAddition.Add(data);
            }
        }

        private async Task connectToPipe(CancellationToken cancellation)
        {
            _logAddition.Add("Waiting...");
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

                Dispatcher.Invoke(() => { _logAddition.Add("📝 " + input); });
            }
        }

        private void onClosing(object? sender, CancelEventArgs e)
        {
            _cancellation.Cancel();
        }
    }
}