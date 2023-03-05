using PL.Simulator123;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO.Packaging;
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


namespace PL.Simulator123;

/// <summary>
/// Interaction logic for Simulator.xaml
/// </summary>
public partial class Simulator123 : Window, INotifyPropertyChanged
{
    BackgroundWorker worker;
    private Stopwatch stopWatch;

    #region PropertyChanged
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

    private DateTime? startTime;
    public string? StartTime => startTime?.ToString("HH:mm:ss");

    private DateTime? estimatedTime;
    public string? EstimatedTime => estimatedTime?.ToString("HH:mm:ss");

    private string? _resultLabelmsg;
    public string? ResultLabelMsg
    {
        get { return _resultLabelmsg; }
        set
        {
            _resultLabelmsg = value;
            OnPropertyChanged(nameof(ResultLabelMsg));
        }
    }

    private string? _timerText;
    public string? TimerText
    {
        get { return _timerText; }
        set
        {
            _timerText = value;
            OnPropertyChanged(nameof(TimerText));
        }
    }

    private int? _estimatedTimeInSec;
    public int? EstimatedTimeInSec
    {
        get { return _estimatedTimeInSec; }
        set
        {
            _estimatedTimeInSec = value;
            OnPropertyChanged(nameof(EstimatedTimeInSec));
        }
    }
    #endregion
    //public int? Progress => (int)(((DateTime.Now - startTime)?.TotalSeconds / _estimatedTimeInSec ?? 1) * 100);

    public Simulator123()
    {
        //ResultLabelMsg = "0 %";
        TimerText = "00:00:00";
        InitializeComponent();
        stopWatch = new Stopwatch();
        worker = new BackgroundWorker();
        worker.DoWork += Worker_DoWork;
        worker.ProgressChanged += Worker_ProgressChanged;
        worker.RunWorkerCompleted += Worker_RunWorkerCompleted;

        if (worker.IsBusy != true)
            worker.RunWorkerAsync();
        worker.WorkerReportsProgress = true;
        worker.WorkerSupportsCancellation = true;


        //Disables the option to close a window immediately.
        this.Closing += new CancelEventHandler(MyWindow_Closing!);
    }


    /// <summary>
    /// Maks shur the cliant really wants close the window
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    void MyWindow_Closing(object sender, CancelEventArgs e) // Disables the option to close a window immediately.
    {
        MessageBoxResult End = MessageBox.Show("Are you sure?", "ERROR", MessageBoxButton.YesNo, MessageBoxImage.Stop);
        switch (End)
        {
            case MessageBoxResult.Yes:
                worker.CancelAsync(); // Cancel the asynchronous operation.
                break;
            case MessageBoxResult.No:
                e.Cancel= true; 
                break;              
        }
    }

    private void Worker_DoWork(object? sender, DoWorkEventArgs e)
    {
        Simulator.RegistrToStopEvent(stopSimulator);
        Simulator.RegistrToUpdateEvent(updateOrder);
        Simulator.StartSimulator();      
        stopWatch.Start();
       
        while(!worker.CancellationPending)
        {
            for (int i = 0; i <= _estimatedTimeInSec; i++)
            {
                Thread.Sleep(1000);
                worker.ReportProgress((int)(i * 100 / _estimatedTimeInSec)!);
            }        
        }
    }
    public int d { get; set; }
    private void Worker_ProgressChanged(object? sender, ProgressChangedEventArgs e)
    {

        TimerText = stopWatch.Elapsed.ToString();
        TimerText = TimerText.Substring(0, 8);
        d = e.ProgressPercentage;
        //ResultLabelMsg = (d + "%");
        OnPropertyChanged(nameof(d));

        if (e.UserState != null)
        {
            var arg = e.UserState as Tuple<BO.Order, int>;
            var now = DateTime.Now;
            startTime = now;
            EstimatedTimeInSec = arg?.Item2;
            estimatedTime = now + new TimeSpan(0, 0, EstimatedTimeInSec ?? 0);
            OnPropertyChanged(nameof(StartTime));
            OnPropertyChanged(nameof(EstimatedTime));
            OrderID = arg!.Item1.ID;
            CurrentStatus = arg!.Item1.Status;
            NextStatus = arg.Item1.Status == BO.Enums.OrderStatus.Order_Confirmed ? BO.Enums.OrderStatus.Order_Sent : BO.Enums.OrderStatus.Order_Provided;
        }
    }

    private void Worker_RunWorkerCompleted(object? sender, RunWorkerCompletedEventArgs e)
    {
        Simulator.UnRegistrToStopEvent(stopSimulator);
        Simulator.UnRegistrToUpdateEvent(updateOrder);
    }

    /// <summary>
    /// Stop the simulator.
    /// </summary>
    /// <param name="sender"></param>
    /// <param name="e"></param>
    private void StopButton_Click(object sender, RoutedEventArgs e)
    {
        if (worker.WorkerSupportsCancellation == true)
        {
            stopWatch.Stop();
            worker.CancelAsync(); // Cancel the asynchronous operation.
            Simulator.StopSimulator();
            ResultLabelMsg = "Canceled!";
        }
    }
    private void stopSimulator(object? sender,EventArgs e)
    {
        worker.CancelAsync();
    }
    private void updateOrder(object? sender, Tuple<BO.Order,int> e )
    {
        worker.ReportProgress(0,e);
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
