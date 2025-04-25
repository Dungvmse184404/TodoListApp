using System;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using Models.DTOs;
using Models.Entities;

namespace GUI
{
    public partial class DailyTaskDetailWindow : Window
    {
        private DailyTaskService _dailyTaskService = new();
        public DailyTaskDetailWindow()
        {
            InitializeComponent();
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
                DailyTaskDto dailyTask = new DailyTaskDto()
                {
                    Title = TitleInput.Text,
                    Description = DescInput.Text,
                    StartDate = new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, StartTime.SelectedTime.Value.Hour, StartTime.SelectedTime.Value.Minute, StartTime.SelectedTime.Value.Second),
                    DueDate = new DateTime(
                        DateInput.SelectedDate.Value.Year, DateInput.SelectedDate.Value.Month, DateInput.SelectedDate.Value.Day, EndTime.SelectedTime.Value.Hour, EndTime.SelectedTime.Value.Minute, EndTime.SelectedTime.Value.Second),
                };
                await _dailyTaskService.AddDailyTaskAsync(dailyTask);
                MessageBox.Show("Add task successfully !");
                this.Close();
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
            return true;
        }
    }
}
