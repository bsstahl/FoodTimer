using Microsoft.AspNetCore.Blazor;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace FoodTimer.Web.Services
{
    public class AppState
    {
        // Actual state
        public int StartingTimeSeconds { get; private set; }
        public bool IsCounting { get; private set; }
        public int CurrentCount { get; private set; }


        private System.Timers.Timer _timer;

        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        public AppState()
        {
            this.StartingTimeSeconds = 1500;
            this.CurrentCount = this.StartingTimeSeconds;

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
        }

        public string BackgroundStyle()
        {
            string result = "waiting";
            if (this.IsCounting)
                if (this.CurrentCount <= 0)
                    result = "stopWhenSatisfied";
                else if (this.CurrentCount <= 600)
                    result = "eatNow";
                else if (this.CurrentCount <= 900)
                    result = "pauseEating";
                else
                    result = "eatNow";

            return result;
        }


        private void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            this.CurrentCount--;
            NotifyStateChanged();
        }

        public async Task Reset()
        {
            this.CurrentCount = this.StartingTimeSeconds;
            await Task.Run(() => NotifyStateChanged());
        }

        public async Task StartStop()
        {
            this.IsCounting = !this.IsCounting;
            if (this.IsCounting)
                _timer.Start();
            else
                _timer.Stop();
            await Task.Run(() => NotifyStateChanged());
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
