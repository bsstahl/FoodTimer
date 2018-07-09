using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FoodTimer.Web.ViewModels
{
    public class TimerModel
    {
        // private System.Timers.Timer _timer;

        public int CurrentCount { get; set; }

        //public TimerModel()
        //{
        //    this.CurrentCount = 900;
        //    _timer = new System.Timers.Timer(1000);
        //    _timer.Elapsed += timerElapsed;
        //}

        //private void timerElapsed(object sender, System.Timers.ElapsedEventArgs e)
        //{
        //    this.CurrentCount--;
        //}

    }
}
