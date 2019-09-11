using System;
using System.Diagnostics;
using System.Windows;
using System.Windows.Threading;

namespace StopWatch
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public DispatcherTimer dispatcherTimer;
        private bool isRunning;
        private Stopwatch stopwatch;

        public MainWindow()
        {
            InitializeComponent();

            // Use the dispatcher timer to update the GUI
            dispatcherTimer = new DispatcherTimer();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 1);

            // The stopwatch itself tracks the time
            stopwatch = new Stopwatch();

            // This bool indicates if the stopwatch is currently running or not
            IsRunning = false;

            resetTimeLabel();
        }

        // For every tick (second), the dispatcher updates the time label
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            lblTime.Content = String.Format("{0:2}:{1:2}:{2:2}", stopwatch.Elapsed.Hours.ToString("D2"), 
                stopwatch.Elapsed.Minutes.ToString("D2"), stopwatch.Elapsed.Seconds.ToString("D2"));
        }

        public bool IsRunning
        {
            get => isRunning;
            set
            {                
                isRunning = value;
                btnStartStop.Content = (isRunning == true) ? "Stop" : "Start";
            }
        }

        private void btnReset_Click(object sender, RoutedEventArgs e)
        {
            stopwatch.Restart();
            IsRunning = false;
            resetTimeLabel();
        }

        private void resetTimeLabel()
        {
            // Init the start value of the time label
            lblTime.Content = "00:00:00";
        }

        private void btnStartStop_Click(object sender, RoutedEventArgs e)
        {
            IsRunning = (IsRunning == true) ? false : true;

            if (!IsRunning)
            {
                stopwatch.Stop();
                dispatcherTimer.Stop();
            }
            else
            {
                stopwatch.Start();
                dispatcherTimer.Start();
            }
        }
    }
}
