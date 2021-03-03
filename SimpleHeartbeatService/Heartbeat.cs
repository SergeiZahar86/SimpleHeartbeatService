using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;

namespace SimpleHeartbeatService
{
    public class Heartbeat
    {
        private readonly Timer _timer;

        public Heartbeat()
        {
            _timer = new Timer(1000)
            {
                AutoReset = true
            };
            _timer.Elapsed += TimerElapsed;
        }

        private void TimerElapsed(object sender, ElapsedEventArgs e)
        {
            string[] lines = new string[]
            {
                DateTime.Now.ToString()
            };
            File.AppendAllLines(@"C:/VisualStudioProject/2021/2021_SERVESES"+
                "/SimpleHeartbeatService/SimpleHeartbeatService/Heartbeat.txt", lines);
        }

        public void Start()
        {
            _timer.Start();
        }

        public void Stop()
        {
            _timer.Stop();
        }
    }
}
