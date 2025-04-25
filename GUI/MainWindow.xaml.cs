using System.Reflection;
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
        private DailyTaskService _dailyTaskService = new DailyTaskService();

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
            LoadDailyTasksForWeek(_currentTime);
        }

        private async void LoadDailyTasksForWeek(DateTime date)
        {
            DateTime monday = date.AddDays(-((int)date.DayOfWeek - 1));

            var list = await _dailyTaskService.GetAllDailyTasksAsync(monday, monday.Add(TimeSpan.FromDays(6)));

            LoadDailyTasks(list, date);
        }

        private void LoadDailyTasks(List<DailyTask> dailyTasks, DateTime date)
        {
            DateTime monday = date.AddDays(-((int)date.DayOfWeek - 1));

            foreach (DailyTask task in dailyTasks)
            {
                var border = CreateTaskBox(task);
                if (task.StartDate.Value.Date == monday.Date)
                {
                    MondayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(1)).Date)
                {
                    TuesdayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(2)).Date)
                {
                    WednesdayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(3)).Date)
                {
                    ThursdayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(4)).Date)
                {
                    FridayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(5)).Date)
                {
                    SaturdayColumnCanvas.Children.Add(border);
                }
                else if (task.StartDate.Value.Date == monday.Add(TimeSpan.FromDays(6)).Date)
                {
                    SundayColumnCanvas.Children.Add(border);
                }
            }
        }

        private Border CreateTaskBox(DailyTask dailyTask)
        {
            Border border = new Border()
            {
                Width = MondayColumn.ActualWidth - 8,
                Height = CalculateTaskBoxHeight(dailyTask.StartDate.Value, dailyTask.DueDate.Value)
            };
            border.Style = (Style)Application.Current.FindResource("BorderTasksContainer");

            Grid grid = new Grid();
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });
            grid.RowDefinitions.Add(new RowDefinition());
            grid.RowDefinitions.Add(new RowDefinition { Height = GridLength.Auto });

            TextBlock startTime = new TextBlock()
            {
                Text = $"{dailyTask.StartDate.Value:HH:mm}",
                Style = (Style)Application.Current.FindResource("SubText"),
                Margin = new Thickness(0, 2, 0, 0),
            };

            TextBlock title = new TextBlock();
            title.Style = (Style)Application.Current.FindResource("NormalText");
            title.Text = dailyTask.Title;
            title.TextAlignment = TextAlignment.Center;
            title.Margin = new Thickness(0, 10, 0, 10);

            TextBlock endTime = new TextBlock()
            {
                Text = $"{dailyTask.DueDate.Value:HH:mm}",
                Style = (Style)Application.Current.FindResource("SubText"),
                Margin = new Thickness(0, 2, 0, 0)
            };

            StackPanel desc = new StackPanel()
            {
                Width = 400
            };
            TextBlock textBlock = new TextBlock()
            {
                Text = "Description",
                Style = (Style)Application.Current.FindResource("NormalText"),
                FontWeight = FontWeights.Bold
            };
            TextBlock descDetail = new TextBlock()
            {
                Text = dailyTask.Description,
                Style = (Style)Application.Current.FindResource("NormalText"),
                Margin = new Thickness(0, 10, 0, 10),
                TextWrapping = TextWrapping.Wrap    
            };
            desc.Children.Add(textBlock);
            desc.Children.Add(descDetail);

            border.ToolTip = desc;

            Grid.SetRow(startTime, 0);
            Grid.SetRow(title, 1);
            Grid.SetRow(endTime, 2);
            grid.Children.Add(startTime);
            grid.Children.Add(title);
            grid.Children.Add(endTime);

            Canvas.SetLeft(border, 4);
            Canvas.SetTop(border, CalculateTaskBoxPosition(dailyTask.StartDate.Value));

            border.Child = grid;

            return border;
        }

        private double CalculateTaskBoxPosition(DateTime time)
        {
            TimeSpan currentTime = time.TimeOfDay;
            TimeSpan totalDay = TimeSpan.FromHours(24);
            double ratio = currentTime.TotalSeconds / totalDay.TotalSeconds;

            return MondayColumnCanvas.ActualHeight * ratio;
        }

        private double CalculateTaskBoxHeight(DateTime fromTime, DateTime toTime)
        {
            TimeSpan diff = toTime - fromTime;
            TimeSpan total = TimeSpan.FromHours(24);
            double ratio = diff.TotalSeconds / total.TotalSeconds;

            return MondayColumnCanvas.ActualHeight * ratio;
        }

        private void RemoveTasksDisplay()
        {
            MondayColumnCanvas.Children.Clear();
            TuesdayColumnCanvas.Children.Clear();
            WednesdayColumnCanvas.Children.Clear();
            ThursdayColumnCanvas.Children.Clear();
            FridayColumnCanvas.Children.Clear();
            SaturdayColumnCanvas.Children.Clear();
            SundayColumnCanvas.Children.Clear();
        }

        private void BackWeekBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(-7));
            RemoveTasksDisplay();
            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
        }

        private void ForwardWeekBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(7));
            RemoveTasksDisplay();
            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
        }

        private void BackMonthBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(-30));
            RemoveTasksDisplay();
            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
        }

        private void ForwardMonthBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            _currentTime = _currentTime.Add(TimeSpan.FromDays(30));
            RemoveTasksDisplay();
            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
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

        private void AddDailyTaskBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DailyTaskDetailWindow dailyTaskDetailWindow = new DailyTaskDetailWindow();  
            dailyTaskDetailWindow.ShowDialog();
        }
    }
}