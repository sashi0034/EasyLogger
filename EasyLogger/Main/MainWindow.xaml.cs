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
        private readonly FilteringModel _filteringModel = new();
        private readonly LoggingList _loggingList = new();
        private readonly FilteringProcess _filteringProcess;
        private readonly LogTaker _logTaker;
        private readonly PipeConnection _pipeConnection;

        public MainWindow()
        {
            InitializeComponent();

            Title = $"EasyLogger [{DateTime.Now:HH:mm:ss}]";

            _filteringProcess = new FilteringProcess(_filteringModel, filteringStack, _loggingList);
            _filteringProcess.Setup();
            _logTaker = new LogTaker(_loggingList, loggingStackPanel, _filteringProcess);

            _pipeConnection = new PipeConnection(Dispatcher, _logTaker);
            _pipeConnection.StartAsync(_cancellation.Token).RunErrorHandler();
        }

        private void onClosing(object? sender, CancelEventArgs e)
        {
            _cancellation.Cancel();
        }
    }
}