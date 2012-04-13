using System.Windows;
using System.Windows.Media.Animation;
using Pomodoro;

namespace PomodoroApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        readonly PomodoroTimer Model = new PomodoroTimer();

        public MainWindow()
        {
            InitializeComponent();
            InitializeComponent();
            DataContext = Model;

            var flashTimer = (Storyboard)FindResource("FlashTimer");
            Model.TimerFinished += (s, e) => flashTimer.Begin();
            Model.Start.CanExecuteChanged += (s, e) => flashTimer.Stop();
        }
    }
}
