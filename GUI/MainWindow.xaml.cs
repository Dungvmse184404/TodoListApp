using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;
using System.Windows.Threading;
using BLL.Services;
using Models.Entities;
using Label = Models.Entities.Label;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private TimeSpan _currentTimeTracker = TimeSpan.Zero;
        private DispatcherTimer _timer = new();
        private DateTime _currentTime = DateTime.Now;
        private TodoTaskService _todoTaskService;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTimeNow();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimeTracker_Tick;
            RealTime_Clock();
            LoadDayInWeek(_currentTime);
        }

        private void LoadDailyTasks()
        {

        }

        private void CreateTaskBox(DailyTask dailyTask)
        {
            Border border = new Border()
            {
                Width = MondayColumnCanvas.ActualWidth - 8
            };
            border.Style = (Style)Application.Current.FindResource("BorderTasksContainer");

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock startTime = new TextBlock()
            {
                Text = $"{dailyTask.StartDate.Value:HH:mm}",
                Style = (Style)Application.Current.FindResource("NormalText"),
                Margin = new Thickness(0, 5, 0, 5)
            };

            TextBlock title = new TextBlock();
            title.Style = (Style)Application.Current.FindResource("NormalText");
            title.Text = dailyTask.Title;
            title.TextAlignment = TextAlignment.Center;
            title.Margin = new Thickness(0, 10, 0, 10);

            TextBlock endTime = new TextBlock()
            {
                Text = $"{dailyTask.DueDate.Value:HH:mm}",
                Style = (Style)Application.Current.FindResource("NormalText"),
                Margin = new Thickness(0, 5, 0, 5)
            };

            Grid.SetRow(startTime, 0);
            Grid.SetRow(title, 1);
            Grid.SetRow(endTime, 2);
            grid.Children.Add(startTime);
            grid.Children.Add(title);
            grid.Children.Add(endTime);

            Canvas.SetLeft(border, 4);
            Canvas.SetTop(border, CalculateTaskBoxPosition());

            border.Child = grid;
            MondayColumnCanvas.Children.Add(border);
        }

        private double CalculateTaskBoxPosition()
        {
            TimeSpan currentTime = DateTime.Now.TimeOfDay;
            TimeSpan totalDay = TimeSpan.FromHours(24);
            double ratio = currentTime.TotalSeconds / totalDay.TotalSeconds;

            return MondayColumnCanvas.ActualHeight * ratio;
        }

        private void BackWeekBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(-7));
            LoadDayInWeek(_currentTime);
        }

        private void ForwardWeekBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(7));
            LoadDayInWeek(_currentTime);
        }

        private void BackMonthBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(-30));
            LoadDayInWeek(_currentTime);
        }

        private void ForwardMonthBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(30));
            LoadDayInWeek(_currentTime);
        }

        private void LoadDayInWeek(DateTime date)
        {
            // 0 1 2 3 4 5 6 
            //       t5
            DateTime monday = date.AddDays(-((int)date.DayOfWeek - 1));

            MonthLabel.Text = $"{date:MMMM}";

            MonLabel.Text = $"{monday:ddd dd}";
            TueLabel.Text = $"{monday.Add(TimeSpan.FromDays(1)):ddd dd}";
            WedLabel.Text = $"{monday.Add(TimeSpan.FromDays(2)):ddd dd}";
            ThuLabel.Text = $"{monday.Add(TimeSpan.FromDays(3)):ddd dd}";
            FriLabel.Text = $"{monday.Add(TimeSpan.FromDays(4)):ddd dd}";
            SatLabel.Text = $"{monday.Add(TimeSpan.FromDays(5)):ddd dd}";
            SunLabel.Text = $"{monday.Add(TimeSpan.FromDays(6)):ddd dd}";
        }

        private void TimeTrackerPlayBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _timer.Start();
        }

        private void TimeTrackerResetBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _timer.Stop();
            _currentTimeTracker = TimeSpan.Zero;
            TimeTracker.Text = "00:00:00";
        }

        private void TimeTrackerStopBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _timer.Stop();
        }

        private void TimeTracker_Tick(object sender, EventArgs e)
        {
            _currentTimeTracker = _currentTimeTracker.Add(TimeSpan.FromSeconds(1));
            TimeTracker.Text = _currentTimeTracker.ToString(@"hh\:mm\:ss");
        }

        private void RealTime_Clock()
        {
            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += RealTime_Tick;
            timer.Start();
        }

        private void RealTime_Tick(object sender, EventArgs e)
        {
            RealTime.Text = $"{DateTime.Now:HH:mm}";
            RealDate.Text = $"{DateTime.Now:ddd dd/MM/yyyy}";
        }

        private void UpdateTimeNow()
        {
            RealTime.Text = $"{DateTime.Now:HH:mm}";
            RealDate.Text = $"{DateTime.Now:ddd dd/MM/yyyy}";
        }

        private void CloseBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void MinimizeBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
    }
}