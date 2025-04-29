using System.Windows;
using BLL.Interfaces;
using BLL.Services;
using Microsoft.IdentityModel.Tokens;
using Label = Models.Entities.Label;

namespace GUI
{
    public partial class LabelDetailWindow : Window
    {
        private ILabelService _labelService = new LabelService();
        public Label CurrentLabel { get; set; } = null;

        public LabelDetailWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (CurrentLabel != null)
            {
                NameInput.Text = CurrentLabel.LabelName;
                DescInput.Text = CurrentLabel.Description;
            }
        }

        private async void SaveBtn_Click(object sender, RoutedEventArgs e)
        {
            if (CheckName() && CheckDescription())
            {
                Label label = new Label()
                {
                    LabelName = NameInput.Text,
                    Description = DescInput.Text
                };
                if (CurrentLabel != null)
                {
                    label.LabelId = CurrentLabel.LabelId;
                    await _labelService.UpdateLabelAsync(label);
                    MessageBox.Show("Update label successfully !");
                } else
                {
                    await _labelService.AddLabelAsync(label);
                    MessageBox.Show("Add label successfully !");
                }
                this.Close();
            }
        }

        private void CancelBtn_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void CloseBtn_MouseUp(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            this.Close();
        }

        private bool CheckName()
        {
            if (NameInput.Text.IsNullOrEmpty())
            {
                MessageBox.Show("Label's name cannot be empty !", "Warning message", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (NameInput.Text.Length < 4)
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

        private void MainBorder_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            if (e.OriginalSource == MainBorder)
            {
                this.DragMove();
            }
        }
    }
}
