using System.Windows;
using System.Windows.Controls;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using Models.Entities;
using Models.Message;

namespace GUI
{
    public partial class SubtaskWindow : Window
    {
        private ITodoTaskService _todoTaskService = new TodoTaskService();
        private ISubTaskService _subTaskService = new SubTaskService();
        public TodoTask TodoTask { get; set; } = null;
        public int? CurrentLabelId { get; set; }

        public SubtaskWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            StatusInput.ItemsSource = Enum.GetValues(typeof(LabelStatus));
            if (TodoTask != null)
            {
                TitleShow.Text = TodoTask.Title;
                StatusInput.SelectedValue = (LabelStatus)Enum.Parse(typeof(LabelStatus), TodoTask.Status); ;
                StartTime.Text = TodoTask.StartDate.ToString();
                EndTime.Text = TodoTask.DueDate.ToString();
                DescInput.Text = TodoTask.Description;
                DescShow.Text = TodoTask.Description;
                ListSubtasks.ItemsSource = TodoTask.SubTasks;
            } else
            {
                StartTime.IsEnabled = true;
                EndTime.IsEnabled = true;
                EditStartDate.Visibility = Visibility.Hidden;
                EditEndDate.Visibility = Visibility.Hidden;
                DescShow.Visibility = Visibility.Collapsed;
                DescInput.Visibility = Visibility.Visible;
                SubtasksContainer.Visibility = Visibility.Collapsed;    
                EditDesct.Visibility = Visibility.Hidden;
                SaveBtn.Visibility = Visibility.Visible;
                TitleInput.Visibility = Visibility.Visible;
                TitleShow.Visibility = Visibility.Collapsed;
                DeleteBtn.Visibility = Visibility.Collapsed;
            }
        }

        private void CloseBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private void EditStartDate_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            StartTime.IsEnabled = true;
            SaveStartTime.Visibility = Visibility.Visible;
        }

        private void EditEndDate_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            EndTime.IsEnabled = true;
            SaveEndTime.Visibility = Visibility.Visible;
        }

        private void EditDesct_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DescInput.Visibility = Visibility.Visible;
            DescShow.Visibility = Visibility.Collapsed;
            DescSave.Visibility = Visibility.Visible;
        }

        private async void SaveStartTime_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TodoTask.StartDate = StartTime.SelectedDate;
            await _todoTaskService.UpdateTodoTaskAsync(TodoTask);
            StartTime.IsEnabled = false;
            SaveStartTime.Visibility = Visibility.Hidden;
        }

        private async void SaveEndTime_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TodoTask.DueDate = EndTime.SelectedDate;
            await _todoTaskService.UpdateTodoTaskAsync(TodoTask);
            EndTime.IsEnabled = false;
            SaveEndTime.Visibility = Visibility.Hidden;
        }

        private async void DescSave_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            TodoTask.Description = DescInput.Text;
            await _todoTaskService.UpdateTodoTaskAsync(TodoTask);
            DescShow.Text = DescInput.Text;
            DescInput.Visibility = Visibility.Collapsed;
            DescShow.Visibility = Visibility.Visible;
            DescSave.Visibility = Visibility.Hidden;
        }

        private async void StatusInput_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (TodoTask != null)
            {
                TodoTask.Status = ((LabelStatus)StatusInput.SelectedItem).ToString();
                await _todoTaskService.UpdateTodoTaskAsync(TodoTask);
            }
        }

        private async void CheckBox_Checked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var subTask = (SubTask)checkBox.DataContext;
            subTask.IsCompleted = true;
            await _subTaskService.UpdateSubTaskAsync(subTask);
        }

        private async void CheckBox_Unchecked(object sender, RoutedEventArgs e)
        {
            CheckBox checkBox = (CheckBox)sender;
            var subTask = (SubTask)checkBox.DataContext;
            subTask.IsCompleted = false;
            await _subTaskService.UpdateSubTaskAsync(subTask);
        }

        private async void AddSubtask_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            AddSubTaskWindow addSubTaskWindow = new AddSubTaskWindow();
            addSubTaskWindow.TodoTaskId = TodoTask.TodoTaskId;
            addSubTaskWindow.ShowDialog();

            TodoTask = await _todoTaskService.GetTodoTaskByIdAsync(TodoTask.TodoTaskId);
            ListSubtasks.ItemsSource = TodoTask.SubTasks;
        }

        private async void DeleteSubTaskBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Image? image = sender as Image;
            if (image != null)
            {
                var subTask = image.Tag as SubTask;
                if (subTask != null)
                {
                    var result = MessageBox.Show("Do you really want to delete this ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
                    if (result == MessageBoxResult.Yes)
                    {
                        await _subTaskService.DeleteSubTaskAsync(subTask.SubTaskId);
                        TodoTask = await _todoTaskService.GetTodoTaskByIdAsync(TodoTask.TodoTaskId);
                        ListSubtasks.ItemsSource = TodoTask.SubTasks;
                    }
                }
            }
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckTitle() && CheckDescription() && CurrentLabelId != null)
            {
                var newTodoTask = new TodoTask()
                {
                    Title = TitleInput.Text,
                    Description = DescInput.Text,
                    CreatedDate = DateTime.Now,
                    StartDate = StartTime.SelectedDate,
                    DueDate = EndTime.SelectedDate,
                    Status = StatusInput.SelectedItem != null ? ((LabelStatus)StatusInput.SelectedItem).ToString() : "InProgress",
                    LabelId = CurrentLabelId.Value
                };
                await _todoTaskService.AddTodoTaskAsync(newTodoTask);
                this.Close();   
            }
        }

        private bool CheckTitle()
        {
            if (TitleInput.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Label's name cannot be empty !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (TitleInput.Text.Length < 4)
            {
                MessageBox.Show("Label's name must be longer than four characters !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
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

        private async void DeleteBtn_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Do you really want to delete this ?", "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning);
            if (result == MessageBoxResult.Yes)
            {
                await _todoTaskService.DeleteTodoTaskAsync(TodoTask.TodoTaskId);
            }

            this.Close();
        }
    }
}
