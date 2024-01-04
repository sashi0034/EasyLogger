using System;
using System.ComponentModel;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using SimpleLogger.Main.Control;
using SimpleLogger.Main.Data;
using SimpleLogger.Main.Present;
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
        private readonly LoggedData _loggedData = new();
        private readonly LogTaker _logTaker;
        private readonly PipeConnection _pipeConnection;

        public MainWindow()
        {
            InitializeComponent();

            _logTaker = new LogTaker(_loggedData, stackPanel);

            _pipeConnection = new PipeConnection(Dispatcher, _logTaker);
            _pipeConnection.StartAsync(_cancellation.Token).RunErrorHandler();
        }

        private void onClosing(object? sender, CancelEventArgs e)
        {
            _cancellation.Cancel();
        }
    }
}