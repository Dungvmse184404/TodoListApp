using System.Windows;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;

namespace GUI
{
    public partial class AddSubTaskWindow : Window
    {
        private ISubTaskService _subTaskService = new SubTaskService();
        public int TodoTaskId { get; set; } 

        public AddSubTaskWindow()
        {
            InitializeComponent();
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckDescription())
            {
                SubTask subTask = new SubTask()
                {
                    Description = DescInput.Text,
                    IsCompleted = false,
                    TodoTaskId = TodoTaskId
                };
                await _subTaskService.AddSubTaskAsync(subTask);
                this.Close();
            }
        }

        private void CloseBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
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
    }
}
