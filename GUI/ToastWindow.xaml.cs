using System.Windows;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;

namespace GUI
{
    public partial class ToastWindow : Window
    {
        public ToastWindow(string title, string message)
        {
            InitializeComponent();

            TitleBlock.Text = title;
            MessageBlock.Text = message;

            var workingArea = SystemParameters.WorkArea;
            this.Left = workingArea.Right - this.Width - 10;
            this.Top = workingArea.Bottom - this.Height - 10;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            PlaySound();
            var anim = new DoubleAnimation(0, 1, TimeSpan.FromSeconds(0.3));
            this.BeginAnimation(OpacityProperty, anim);

            DispatcherTimer timer = new DispatcherTimer();
            timer.Interval = TimeSpan.FromSeconds(4);
            timer.Tick += (s, args) =>
            {
                timer.Stop();
                CloseWithAnimation();
            };
            timer.Start();
        }

        private void CloseWithAnimation()
        {
            var anim = new DoubleAnimation(1, 0, TimeSpan.FromSeconds(0.3));
            anim.Completed += (s, e) => this.Close();
            this.BeginAnimation(OpacityProperty, anim);
        }

        private void PlaySound()
        {
            try
            {
                MediaPlayer player = new MediaPlayer();
                player.Open(new Uri("Assets/mixkit-happy-bells-notification-937.wav", UriKind.Relative));
                player.Volume = 0.3;
                player.Play();
            }
            catch (Exception ex)
            {
                
            }
        }
    }
}
