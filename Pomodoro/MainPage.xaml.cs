using System.Windows.Controls;

namespace Pomodoro
{
    public partial class MainPage : UserControl
    {
        PomodoroTimer Model = new PomodoroTimer();

        public MainPage()
        {
            InitializeComponent();
            DataContext = Model;
            Model.TimerFinished += (s, e) => FlashTimer.Begin();
            Model.Start.CanExecuteChanged += (s, e) => FlashTimer.Stop();
        }
    }
}
