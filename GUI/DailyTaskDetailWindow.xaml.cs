using System.Windows;
using System.Windows.Input;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;

namespace GUI
{
    public partial class DailyTaskDetailWindow : Window
    {
        private IDailyTaskService _dailyTaskService = new DailyTaskService();
        public DailyTask DailyTask { get; set; } = null;

        public DailyTaskDetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (DailyTask != null)
            {
                TitleInput.Text = DailyTask.Title;
                DescInput.Text = DailyTask.Description;
                DateInput.SelectedDate = DailyTask.StartDate;
                StartTime.SelectedTime = DailyTask.StartDate;
                EndTime.SelectedTime = DailyTask.DueDate;
            }
        }

        private void CloseBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTitle() && CheckDescription() && CheckDate() && CheckTime())
            {
                if (await _dailyTaskService.IsAvailabeSlotAsync(new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, StartTime.SelectedTime.Value.Hour, StartTime.SelectedTime.Value.Minute, StartTime.SelectedTime.Value.Second), new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, EndTime.SelectedTime.Value.Hour, EndTime.SelectedTime.Value.Minute, EndTime.SelectedTime.Value.Second)))
                {
                    DailyTask dailyTask = new DailyTask()
                    {
                        Title = TitleInput.Text,
                        Description = DescInput.Text,
                        StartDate = new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, StartTime.SelectedTime.Value.Hour, StartTime.SelectedTime.Value.Minute, StartTime.SelectedTime.Value.Second),
                        DueDate = new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, EndTime.SelectedTime.Value.Hour, EndTime.SelectedTime.Value.Minute, EndTime.SelectedTime.Value.Second),
                    };
                    if (DailyTask == null)
                    {
                        await _dailyTaskService.AddDailyTaskAsync(dailyTask);
                        MessageBox.Show("Add task successfully !");
                    }
                    else
                    {
                        dailyTask.DailyTasksId = DailyTask.DailyTasksId;
                        await _dailyTaskService.UpdateDailyTaskAsync(dailyTask);
                        MessageBox.Show("Update task successfully !");
                    }
                    this.Close();
                }
                else
                {
                    MessageBox.Show("This time is invalid !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }

        private bool CheckTitle()
        {
            if (TitleInput.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Title cannot be empty !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TitleInput.Text.Length < 4)
            {
                MessageBox.Show("Title must be longer than four characters !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool CheckDescription()
        {
            if (DescInput.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Description cannot be empty !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            return true;
        }

        private bool CheckDate()
        {
            if (DateInput.SelectedDate == null)
            {
                MessageBox.Show("Date cannot be empty !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (DateInput.SelectedDate < DateTime.Now.Date)
            {
                MessageBox.Show("Cannot select time in the past !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private bool CheckTime()
        {
            if (StartTime.SelectedTime == null)
            {
                MessageBox.Show("You need to choose the start time !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (EndTime.SelectedTime == null)
            {
                MessageBox.Show("You need to choose the end time !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }

            if (DateInput.SelectedDate == DateTime.Now.Date)
            {
                if (StartTime.SelectedTime.Value.TimeOfDay < DateTime.Now.TimeOfDay)
                {
                    MessageBox.Show("Cannot select time in the past !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
                if (StartTime.SelectedTime >= EndTime.SelectedTime)
                {
                    MessageBox.Show("The end time cannot be earlier than start time !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                    return false;
                }
            }

            return true;
        }
    }
}
