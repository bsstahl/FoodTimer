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


        // Lets components receive change notifications
        // Could have whatever granularity you want (more events, hierarchy...)
        public event Action OnChange;

        public AppState()
        {
            this.StartingTimeSeconds = 900;
            this.CurrentCount = this.StartingTimeSeconds;
        }

        public async Task StartStop()
        {
            // this.IsCounting = !this.IsCounting;
            Tick();
            await Task.Run(() => NotifyStateChanged());
        }

        public void Tick()
        {
            this.CurrentCount--;
            NotifyStateChanged();
        }

        private void NotifyStateChanged() => OnChange?.Invoke();
    }
}
