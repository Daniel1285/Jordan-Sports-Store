using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PL.Simulator;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
public partial class Simulator : Window, INotifyPropertyChanged
{
    BackgroundWorker worker;
    private Stopwatch stopWatch;
    private bool isTimerRun;
    BackgroundWorker timerworker;

    // ----------------------------> PropertyChanged <------------------------------

    public event PropertyChangedEventHandler? PropertyChanged;
    private void OnPropertyChanged(string propertyName)
    {
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
    }

    private int _orderID;
    public int OrderID
    {
        get { return _orderID; } 
        set 
        { 
            _orderID = value;
            OnPropertyChanged(nameof(OrderID));
        }   
    }

    private BO.Enums.OrderStatus _currentStatus;
    public BO.Enums.OrderStatus CurrentStatus
    {
        get { return _currentStatus; }
        set
        {
            _currentStatus = value;
            OnPropertyChanged(nameof(CurrentStatus));
        }
    }

    private BO.Enums.OrderStatus _nextStatus;
    public BO.Enums.OrderStatus NextStatus
    {
        get { return _nextStatus; }
        set
        {
            _nextStatus = value;
            OnPropertyChanged(nameof(NextStatus));
        }
    }



    // ----------------------------> End PropertyChanged <------------------------------

    public Simulator()
    {
        InitializeComponent();  
    }


































    //public Simulator()
    //{
    //    InitializeComponent();
    //    // For ProgressBar.
    //    worker = new BackgroundWorker();
    //    worker.DoWork += Worker_DoWork!;
    //    worker.ProgressChanged += Worker_ProgressChanged!;
    //    worker.RunWorkerCompleted += Worker_RunWorkerCompleted!;
    //    worker.WorkerReportsProgress = true;
    //    worker.WorkerSupportsCancellation = true;

    //    // For clock.
    //    stopWatch = new Stopwatch();
    //    timerworker = new BackgroundWorker();
    //    timerworker.DoWork += Worker_DoWork1!;
    //    timerworker.ProgressChanged += Worker_ProgressChanged1!;
    //    timerworker.WorkerReportsProgress = true;

    //    // start the simulator.
    //    if (worker.IsBusy != true)
    //        worker.RunWorkerAsync(8);

    //    if (!isTimerRun)
    //    {
    //        stopWatch.Start();
    //        isTimerRun = true;
    //        timerworker.RunWorkerAsync();
    //    }

    //    //Disables the option to close a window immediately.
    //    this.Closing += new CancelEventHandler(MyWindow_Closing!);
    //}


    //void MyWindow_Closing(object sender, CancelEventArgs e)
    //{
    //    MessageBoxResult End = MessageBox.Show("Are you sure?", "ERROR", MessageBoxButton.YesNo, MessageBoxImage.Stop);
    //    switch (End)
    //    {
    //        case MessageBoxResult.Yes:
    //            worker.CancelAsync(); // Cancel the asynchronous operation.
    //            break;
    //    }
    //}

    //// ------------------------------------------------> Event of ProgressBar <--------------------------------------------------------

    ///// <summary>
    ///// Worker_DoWork for ProgressBar.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void Worker_DoWork(object sender, DoWorkEventArgs e)
    //{
    //    Stopwatch stopwatch = new Stopwatch();
    //    stopwatch.Start();

    //    // BackgroundWorker worker = sender as BackgroundWorker;

    //    int length = (int)e.Argument!;

    //    for (int i = 1; i <= length; i++)
    //    {
    //        if (worker.CancellationPending == true)
    //        {
    //            e.Cancel = true;
    //            break;
    //        }
    //        else
    //        {
    //            // Perform a time consuming operation and report progress.
    //            Thread.Sleep(500);
    //            if (worker.WorkerReportsProgress == true)
    //                worker.ReportProgress(i * 100 / length);
    //        }
    //    }
    //    e.Result = stopwatch.ElapsedMilliseconds;
    //}

    ///// <summary>
    ///// Worker_ProgressChanged for simulator.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
    //{
    //    int progress = e.ProgressPercentage;
    //    resultLabel.Content = (progress + "%");
    //    resultProgressBar.Value = progress;
    //}

    //private void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
    //{
    //    if (e.Cancelled == true)
    //    {
    //        resultLabel.Content = "Canceled!";
    //    }
    //    else if (e.Error != null)
    //    {
    //        resultLabel.Content = "Error: " + e.Error.Message; // Exception Message
    //    }
    //    else
    //    {
    //        long result = (long)e.Result!;
    //        if (result < 1000)
    //        {
    //            resultLabel.Content = "Done after " + result + " ms.";
    //        }
    //        else
    //        {
    //            resultLabel.Content = "Done after " + result / 1000 + " sec.";
    //            if (worker.IsBusy != true)
    //                worker.RunWorkerAsync(8);

    //        }
    //    }
    //}

    ///// <summary>
    ///// start the ProgressBar.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void StartButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (worker.IsBusy != true)
    //        worker.RunWorkerAsync(5); // Start the asynchronous operation.
    //}

    //// ----------------------------------------> Event for clock <--------------------------------

    ///// <summary>
    ///// Worker_DoWork for Clock
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void Worker_DoWork1(object sender, DoWorkEventArgs e)
    //{
    //    while (isTimerRun)
    //    {
    //        timerworker.ReportProgress(231);
    //        Thread.Sleep(1000);
    //    }
    //}

    ///// <summary>
    ///// Worker_ProgressChanged for Clock.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void Worker_ProgressChanged1(object sender, ProgressChangedEventArgs e)
    //{
    //    string timerText = stopWatch.Elapsed.ToString();
    //    timerText = timerText.Substring(0, 8);
    //    this.timerTextBlock.Text = timerText;
    //}

    ////------------------------------------> End event <-------------------------------

    ///// <summary>
    ///// stop the simulator.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void StopButton_Click(object sender, RoutedEventArgs e)
    //{
    //    if (worker.WorkerSupportsCancellation == true || isTimerRun)
    //    {
    //        stopWatch.Stop();
    //        isTimerRun = false;
    //        worker.CancelAsync(); // Cancel the asynchronous operation.
    //    }
    //}


    ///// <summary>
    ///// back to the main Window.
    ///// </summary>
    ///// <param name="sender"></param>
    ///// <param name="e"></param>
    //private void backToMainWindow_Click(object sender, RoutedEventArgs e)
    //{
    //    new MainWindow().Show();
    //    this.Close();
    //}
}
