using System;
using System.Windows;
using System.Windows.Threading;
using System.ComponentModel;

namespace Pomodoro
{
    public class PomodoroTimer : INotifyPropertyChanged
    {
        public StartCommand Start { get { return new StartCommand(this); } }

        public event PropertyChangedEventHandler PropertyChanged;

        public string TimeRemaining
        {
            get { return timeRemaining; }
            private set
            {
                timeRemaining = value;
                NotifyPropertyChanged("TimeRemaining");
            }
        }

        DispatcherTimer timer;
        DateTime startTime;

        private string timeRemaining;

        void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        void StartTicking()
        {
            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.2)
            };
            timer.Tick += TimerTick;
            startTime = DateTime.Now;
            timer.Start();
        }

        private void TimerTick(object sender, EventArgs e)
        {
            TimeRemaining = (DateTime.Now - startTime).ToString("mm':'ss");
        }

        public class StartCommand : Command
        {
            PomodoroTimer pomodoro;

            public StartCommand(PomodoroTimer pomodoro)
                : base()
            {
                this.pomodoro = pomodoro;
            }

            public override void Execute(object parameter)
            {
                ICanExecute = false;
                pomodoro.StartTicking();
            }
        }
    }
}
