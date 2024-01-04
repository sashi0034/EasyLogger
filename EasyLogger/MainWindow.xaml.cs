using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleLogger.Utils;

namespace SimpleLogger
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly CancellationTokenSource _cancellation = new();

        public MainWindow()
        {
            InitializeComponent();

            connectToPipe(_cancellation.Token).RunErrorHandler();
        }

        public void addLogging(string text)
        {
            var textBox = new TextBox()
            {
                Text = text,
                IsReadOnly = true,
                TextWrapping = TextWrapping.Wrap,
                Margin = new Thickness(8, 2, 8, 2),
            };
            stackPanel.Children.Add(textBox);
        }

        private async Task connectToPipe(CancellationToken cancellation)
        {
            addLogging("Waiting...");
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

                Dispatcher.Invoke(() => { addLogging("📝 " + input); });
            }
        }

        private void onClosing(object? sender, CancelEventArgs e)
        {
            _cancellation.Cancel();
        }
    }
}