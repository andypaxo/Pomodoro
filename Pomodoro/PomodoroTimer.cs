using System;
using System.Windows.Threading;
using System.ComponentModel;

namespace Pomodoro
{
    public class PomodoroTimer : INotifyPropertyChanged
    {
        public StartCommand Start { get; private set; }
        
        public event PropertyChangedEventHandler PropertyChanged;
        public event EventHandler TimerFinished;

        public string TimeRemaining
        {
            get { return timeRemaining; }
            private set
            {
                timeRemaining = value;
                NotifyPropertyChanged("TimeRemaining");
            }
        }

        public string TotalTime
        {
            get { return totalTime; }
            private set
            {
                totalTime = value;
                NotifyPropertyChanged("TotalTime");
            }
        }

        public string State
        {
            get { return state; }
            set {
                state = value;
                NotifyPropertyChanged("State");
            }
        }

        public double ProgressValue
        {
            get { return progressValue; }
            set
            {
                progressValue = value;
                NotifyPropertyChanged("ProgressValue");
            }
        }

        private string timeRemaining;
        private string totalTime;
        private string state;
        private double progressValue;


        public PomodoroTimer()
        {
            Start = new StartCommand(this);
        }

        private DispatcherTimer timer;
        private DateTime startTime;
        private TimeSpan iterationLength;


        void NotifyPropertyChanged(string propName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propName));
        }

        void StartTicking(TimeSpan iterationLength)
        {
            this.iterationLength = iterationLength;
            TotalTime = iterationLength.ToString("mm':'ss");

            timer = new DispatcherTimer
            {
                Interval = TimeSpan.FromSeconds(0.2)
            };
            timer.Tick += TimerTick;
            startTime = DateTime.Now;
            timer.Start();

            State = "Normal";
        }

        private void StopTicking()
        {
            timer.Stop();
            Start.AllowExecute();
            if (TimerFinished != null)
                TimerFinished(this, EventArgs.Empty);

            State = "None";
        }

        private void TimerTick(object sender, EventArgs e)
        {
            var timeDifference = DateTime.Now - startTime;
            TimeRemaining = timeDifference.ToString("mm':'ss");

            ProgressValue = timeDifference.TotalMilliseconds / iterationLength.TotalMilliseconds;

            if (timeDifference >= iterationLength)
                StopTicking();
        }

        public class StartCommand : Command
        {
            readonly PomodoroTimer pomodoro;

            public StartCommand(PomodoroTimer pomodoro)
            {
                this.pomodoro = pomodoro;
            }

            public override void Execute(object parameter)
            {
                ICanExecute = false;
                var time = Convert.ToInt32(parameter);
                pomodoro.StartTicking(TimeSpan.FromMinutes(time));
            }

            public void AllowExecute()
            {
                ICanExecute = true;
            }
        }
    }
}
