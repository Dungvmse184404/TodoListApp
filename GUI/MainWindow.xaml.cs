using ApiExtension.Models.RequestModels;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using ApiExtension.Services;
using BLL.Interfaces;
using BLL.Services;
using Models.Entities;
using Label = Models.Entities.Label;
using System.IO;
using NAudio.Wave;
using ApiExtension.Models.ResponseModels;
using System.Windows.Shell;
using System.Threading.Tasks;
using NAudio.Gui;

namespace GUI
{
    public partial class MainWindow : Window
    {
        private TimeSpan _currentTimeTracker = TimeSpan.Zero;
        private DispatcherTimer _timer = new();
        private DateTime _currentTime = DateTime.Now;
        private ITodoTaskService _todoTaskService = new TodoTaskService();
        private ILabelService _labelService = new LabelService();
        private IDailyTaskService _dailyTaskService = new DailyTaskService();
        private WeatherService _weatherService = new WeatherService();
        private Dictionary<Label, List<TodoTask>> TodoTasksDict { get; set; } = null!;
        private DispatcherTimer _notiTimer = new DispatcherTimer();

        private MediaFoundationReader reader;
        private WaveOutEvent outputDevice;
        private DispatcherTimer musicTimer;
        private MusicService _musicService = new();
        private MusicResponseModel _musicResponseModel = null;
        private bool isStopping = false;

        public MainWindow()
        {
            InitializeComponent();
            UpdateTimeNow();

            WindowChrome windowChrome = new WindowChrome
            {
                ResizeBorderThickness = new Thickness(6),
                CaptionHeight = 0,
                CornerRadius = new CornerRadius(0),
                GlassFrameThickness = new Thickness(0)
            };

            WindowChrome.SetWindowChrome(this, windowChrome);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            _timer.Interval = TimeSpan.FromSeconds(1);
            _timer.Tick += TimeTracker_Tick;
            RealTime_Clock();

            _notiTimer.Interval = TimeSpan.FromSeconds(10);
            _notiTimer.Tick += NotiTimer_Tick;
            _notiTimer.Start();

            LoadWeather();

            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
        }

        private async void NotiTimer_Tick(object sender, EventArgs e)
        {
            TimeSpan now = DateTime.Now.TimeOfDay;
            var listNeedNoti = await _dailyTaskService.GetAllDailyTasksAsync(DateTime.Now, DateTime.Now);
            List<TimeSpan> reminderTimes = new List<TimeSpan>();
            foreach (var task in listNeedNoti)
            {
                if (Math.Abs((now - task.StartDate.Value.TimeOfDay).TotalMinutes) < 1)
                {
                    ShowReminder(task.StartDate.Value.TimeOfDay, task.Title);
                }
            }

            if (DateTime.Now.Hour == 0 && DateTime.Now.Minute == 0)
            {
                listNeedNoti.Clear();
            }
        }

        private void ShowReminder(TimeSpan time, string work)
        {
            string title = "Notification!";
            string message = $"It's {time.Hours:D2}:{time.Minutes:D2}, you need to {work}.";

            var toast = new ToastWindow(title, message);
            toast.Show();
        }

        private void StartImageRotage()
        {
            musicTimer = new DispatcherTimer();
            musicTimer.Interval = TimeSpan.FromMilliseconds(20);
            musicTimer.Tick += Timer_Tick;
            musicTimer.Start(); 
        }

        private void StopImageRotage()
        {
            if (musicTimer != null)
            {
                musicTimer.Stop();
            }
        }

        private async Task<BitmapImage?> LoadImage(string url)
        {
            var imageData = await _musicService.GetImageDate(url);
            if (imageData != null)
            {
                var ms = new MemoryStream(imageData);

                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.StreamSource = ms;
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();
                return bitmap;
            }
            return null;
        }

        private void Volume_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (outputDevice != null)
            {
                outputDevice.Volume = (float)Volume.Value;
            }
        }

        private async void PlayMusicBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (reader != null && outputDevice != null && outputDevice.PlaybackState == PlaybackState.Paused)
            {
                outputDevice.Play();
            } 
            else if (reader == null && outputDevice == null) 
            {
                if (_musicResponseModel == null)
                {
                    _musicResponseModel = await _musicService.GetRandomMusicTracks("lofi");
                    if (_musicResponseModel != null)
                    {
                        var firstTrack = _musicResponseModel.ListTracks[0];
                        PlayMusic(firstTrack);
                    }
                }
            }
        }

        private void PauseMusicBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (reader != null && outputDevice != null && outputDevice.PlaybackState == PlaybackState.Playing)
            {
                outputDevice.Pause();
            }
        }

        private async void NextMusicBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (reader != null && outputDevice != null)
            {
                _musicResponseModel = await _musicService.GetRandomMusicTracks("lofi");
                if (_musicResponseModel != null)
                {
                    var firstTrack = _musicResponseModel.ListTracks[0];
                    PlayMusic(firstTrack);
                }
            }
        }

        private void StopMusicBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            StopMusic();
            _musicResponseModel = null;
        }

        private async void PlayMusic(MusicInfo musicInfo)
        {
            StopMusic();
            StartImageRotage(); 

            MusicImage.ImageSource = await LoadImage(musicInfo.Image);
            MusicName.Text = musicInfo.Name;
            Artist.Text = musicInfo.ArtistName;

            reader = new MediaFoundationReader(musicInfo.Audio);
            outputDevice = new WaveOutEvent();
            outputDevice.Init(reader);
            outputDevice.Play();
            outputDevice.PlaybackStopped += OutputDevice_PlaybackStopped;
        }

        private async void OutputDevice_PlaybackStopped(object sender, StoppedEventArgs e)
        {
            await Dispatcher.InvokeAsync(async () =>
               {
                   if (isStopping)
                   {
                       isStopping = false;
                       return;
                   }

                   if (_musicResponseModel != null && _musicResponseModel.ListTracks.Count > 0)
                   {
                       _musicResponseModel.ListTracks.RemoveAt(0);
                       if (_musicResponseModel.ListTracks.Count > 0)
                       {
                           PlayMusic(_musicResponseModel.ListTracks[0]);
                       }
                       else
                       {
                           _musicResponseModel = await _musicService.GetRandomMusicTracks("lofi");
                           if (_musicResponseModel != null)
                           {
                               var firstTrack = _musicResponseModel.ListTracks[0];
                               PlayMusic(firstTrack);
                           }
                       }
                   }
               });
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (reader != null && outputDevice != null)
            {
                MusicImageRotage.Angle += 1; 
                if (MusicImageRotage.Angle >= 360)
                {
                    MusicImageRotage.Angle = 0;
                }
            }
        }

        private void StopMusic()
        {
            StopImageRotage();

            if (outputDevice != null)
            {
                isStopping = true;

                outputDevice.Stop();
                outputDevice.Dispose();
                outputDevice = null;
            }
            if (reader != null)
            {
                reader.Dispose();
                reader = null;
            }
        }

        private async void LoadWeather()
        {
            var weather = await _weatherService.GetCurrentWeather(new WeatherRequestModel()
            {
                City = "Ho%20Chi%20Minh,VN"
            });
            if (weather != null)
            {
                Temp.Text = $"{weather.MainInfo.Temp} °C";
                FeelTemp.Text = $"Feel: {weather.MainInfo.FeelsLike} °C";
                TempMin.Text = $"{weather.MainInfo.TempMin} °C";
                TempMax.Text = $"{weather.MainInfo.TempMax} °C";
                SetWeatherIconAsync(weather.Weathers[0].Icon);
                TempDesc.Text = weather.Weathers[0].Description;
            }
        }

        private async Task SetWeatherIconAsync(string iconCode)
        {
            var imageData = await _weatherService.GetWeatherIconBytesAsync(iconCode);
            if (imageData != null)
            {
                using (var stream = new MemoryStream(imageData))
                {
                    BitmapImage bitmap = new BitmapImage();
                    bitmap.BeginInit();
                    bitmap.CacheOption = BitmapCacheOption.OnLoad;
                    bitmap.StreamSource = stream;
                    bitmap.EndInit();
                    bitmap.Freeze();

                    WeatherIcon.Source = bitmap;
                }
            }
        }

        private void AddTodoTaskBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            Image? image = sender as Image;
            if (image != null)
            {
                int labelId = (int)image.Tag;
                SubtaskWindow subtaskWindow = new SubtaskWindow();
                subtaskWindow.CurrentLabelId = labelId;
                subtaskWindow.ShowDialog();

                LoadTodoListTask(LabelNameSearchInput.Text);
            }
        }

        private void AddLabelBtn_Click(object sender, RoutedEventArgs e)
        {
            LabelDetailWindow labelDetailWindow = new LabelDetailWindow();
            labelDetailWindow.ShowDialog();

            LoadTodoListTask(LabelNameSearchInput.Text);
        }

        private void LabelSearchBtn_Click(object sender, RoutedEventArgs e)
        {
            LoadTodoListTask(LabelNameSearchInput.Text);
        }

        private void TodoTasksSubList_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListBox listBox)
            {
                var selectedTask = listBox.SelectedItem as TodoTask;
                if (selectedTask != null)
                {
                    SubtaskWindow subtaskWindow = new SubtaskWindow();
                    subtaskWindow.TodoTask = selectedTask;
                    subtaskWindow.ShowDialog();

                    LoadTodoListTask(LabelNameSearchInput.Text);
                }
            }
        }

        private void EditLabelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu? contextMenu = menuItem.Parent as ContextMenu;

            Label? label = contextMenu.Tag as Label;
            if (label != null)
            {
                LabelDetailWindow labelDetailWindow = new LabelDetailWindow();
                labelDetailWindow.CurrentLabel = label;
                labelDetailWindow.ShowDialog();

                LoadTodoListTask(LabelNameSearchInput.Text);
            }
        }

        private async void DeleteLabelMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu? contextMenu = menuItem.Parent as ContextMenu;

            Label? label = contextMenu.Tag as Label;
            if (label != null)
            {
                var result = MessageBox.Show("Do you really want to delete this ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _labelService.DeleteLabelAsync(label.LabelId);
                }

                LoadTodoListTask(LabelNameSearchInput.Text);
            }
        }

        private void LabelInfo_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var stackpanel = sender as StackPanel;

            if (stackpanel != null)
            {
                ContextMenu contextMenu = new ContextMenu
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1)
                };

                contextMenu.Tag = stackpanel.Tag;

                MenuItem item1 = new MenuItem
                {
                    Header = "Edit",
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Assets/edit_source.png")),
                        Width = 16,
                        Height = 16
                    }
                };
                item1.Click += EditLabelMenuItem_Click;

                MenuItem item2 = new MenuItem
                {
                    Header = "Delete",
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_source.png")),
                        Width = 16,
                        Height = 16
                    }
                };
                item2.Click += DeleteLabelMenuItem_Click;

                contextMenu.Items.Add(item1);
                contextMenu.Items.Add(item2);

                stackpanel.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            }
        }

        private async void LoadTodoListTask(string labelName)
        {
            var listTodoTasks = await _todoTaskService.GetAllTodoTasksAsync();
            var listLabels = (await _labelService.GetAllLabelsAsync()).Where(x => x.LabelName.ToLower().Contains(labelName.ToLower())).ToList();

            TodoTasksDict = new();

            foreach (var label in listLabels)
            {
                TodoTasksDict.Add(label, listTodoTasks.Where(x => x.LabelId == label.LabelId).ToList());
            }

            TodoTasksListTable.ItemsSource = TodoTasksDict;
        }

        // Daily task
        private async void LoadDailyTasksForWeek(DateTime date)
        {
            int interval = date.DayOfWeek == DayOfWeek.Sunday ? -6 : -(int)date.DayOfWeek + 1;
            DateTime monday = date.AddDays(interval);

            var list = await _dailyTaskService.GetAllDailyTasksAsync(monday, monday.Add(TimeSpan.FromDays(6)));

            LoadDailyTasks(list, date);
        }

        private void LoadDailyTasks(List<DailyTask> dailyTasks, DateTime date)
        {
            int interval = date.DayOfWeek == DayOfWeek.Sunday ? -6 : -(int)date.DayOfWeek + 1;
            DateTime monday = date.AddDays(interval);

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

        private void DailyTaskBorder_MouseRightButtonUp(object sender, MouseButtonEventArgs e)
        {
            var border = sender as Border;

            if (border != null)
            {
                ContextMenu contextMenu = new ContextMenu
                {
                    Background = Brushes.White,
                    BorderBrush = Brushes.Gray,
                    BorderThickness = new Thickness(1)
                };

                contextMenu.Tag = border.Tag;

                MenuItem item1 = new MenuItem
                {
                    Header = "Edit",
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Assets/edit_source.png")),
                        Width = 16,
                        Height = 16
                    }
                };
                item1.Click += EditMenuItem_Click;

                MenuItem item2 = new MenuItem
                {
                    Header = "Delete",
                    Icon = new Image
                    {
                        Source = new BitmapImage(new Uri("pack://application:,,,/Assets/delete_source.png")),
                        Width = 16,
                        Height = 16
                    }
                };
                item2.Click += DeleteMenuItem_Click;

                contextMenu.Items.Add(item1);
                contextMenu.Items.Add(item2);

                border.ContextMenu = contextMenu;
                contextMenu.IsOpen = true;
            }
        }

        private void EditMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu? contextMenu = menuItem.Parent as ContextMenu;

            DailyTask? dailyTask = contextMenu.Tag as DailyTask;
            if (dailyTask != null)
            {
                DailyTaskDetailWindow dailyTaskDetailWindow = new();
                dailyTaskDetailWindow.DailyTask = dailyTask;
                dailyTaskDetailWindow.ShowDialog();

                RemoveTasksDisplay();
                LoadDayInWeek(_currentTime);
                LoadDailyTasksForWeek(_currentTime);
            }
        }

        private async void DeleteMenuItem_Click(object sender, RoutedEventArgs e)
        {
            MenuItem menuItem = (MenuItem)sender;
            ContextMenu? contextMenu = menuItem.Parent as ContextMenu;

            DailyTask? dailyTask = contextMenu.Tag as DailyTask;
            if (dailyTask != null)
            {
                var result = MessageBox.Show("Do you really want to delete this ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                if (result == MessageBoxResult.Yes)
                {
                    await _dailyTaskService.DeleteDailyTaskAsync(dailyTask.DailyTasksId);
                }

                RemoveTasksDisplay();
                LoadDayInWeek(_currentTime);
                LoadDailyTasksForWeek(_currentTime);
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
            border.Tag = dailyTask;
            border.MouseRightButtonUp += DailyTaskBorder_MouseRightButtonUp;

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
            title.FontSize = 14;
            title.TextWrapping = TextWrapping.Wrap;
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
            int interval = date.DayOfWeek == DayOfWeek.Sunday ? -6 : -(int)date.DayOfWeek + 1;
            DateTime monday = date.AddDays(interval);

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

            LoadDailyTasksForWeek(_currentTime);
        }

        private void CalendarBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            DayTasksContainer.Visibility = Visibility.Visible;
            CalendarBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A4BEF3"));

            TodoTaskContainer.Visibility = Visibility.Collapsed;
            TodoTaskBtn.Background = Brushes.Transparent;

            LoadDayInWeek(_currentTime);
            LoadDailyTasksForWeek(_currentTime);
        }

        private void TodoTaskBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            TodoTaskContainer.Visibility = Visibility.Visible;
            TodoTaskBtn.Background = new SolidColorBrush((Color)ColorConverter.ConvertFromString("#A4BEF3"));

            DayTasksContainer.Visibility = Visibility.Collapsed;
            CalendarBtn.Background = Brushes.Transparent;

            LoadTodoListTask(LabelNameSearchInput.Text);
        }

        private void Border_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.OriginalSource == MainBorder)
            {
                this.DragMove();
            }
        }
    }
}