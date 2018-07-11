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

        public string PanelStyle { get; private set; }


        private System.Timers.Timer _timer;

        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        public AppState()
        {
            this.StartingTimeSeconds = 1500;
            this.CurrentCount = this.StartingTimeSeconds;
            this.PanelStyle = GetBackgroundStyle();

            _timer = new System.Timers.Timer(1000);
            _timer.Elapsed += _timer_Elapsed;
        }

        public async Task Reset()
        {
            await UpdateCounter(this.StartingTimeSeconds);
        }

        public async Task StartStop()
        {
            this.IsCounting = !this.IsCounting;
            if (this.IsCounting)
                _timer.Start();
            else
                _timer.Stop();
            this.PanelStyle = GetBackgroundStyle();
            await Task.Run(() => NotifyStateChanged());
        }

        private async void _timer_Elapsed(object sender, System.Timers.ElapsedEventArgs e)
        {
            await UpdateCounter(this.CurrentCount - 1);
        }

        private async Task UpdateCounter(int newCount)
        {
            this.CurrentCount = newCount;
            this.PanelStyle = GetBackgroundStyle();
            await Task.Run(() => NotifyStateChanged());
        }

        private string GetBackgroundStyle()
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

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
