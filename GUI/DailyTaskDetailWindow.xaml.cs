using System.Windows;
using System.Windows.Input;

namespace GUI
{
    public partial class DailyTaskDetailWindow : Window
    {
        public DailyTaskDetailWindow()
        {
            InitializeComponent();
        }

        private void CloseBtn_MouseUp(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
