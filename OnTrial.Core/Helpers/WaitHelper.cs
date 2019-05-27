using System;
using System.Threading;
using System.Threading.Tasks;

namespace OnTrial.Common
{
    public static class WaitHelper
    {
        public static bool Wait(Func<bool> pCondition, TimeSpan? pTimeout = null, TimeSpan? pSleepInterval = null, string pMessage = null)
        {
            var result = false;
            var start = DateTime.Now;
            var canceller = new CancellationTokenSource();
            var task = Task.Factory.StartNew(pCondition, canceller.Token);
            var timeout = pTimeout ?? TimeSpan.Zero;
            var sleepInterval = pSleepInterval ?? TimeSpan.Zero;

            while ((DateTime.Now - start).TotalSeconds < timeout.TotalSeconds)
            {
                if (task.IsCompleted)
                {
                    if (task.Result)
                    {
                        result = true;
                        canceller.Cancel();
                        break;
                    }

                    task = Task.Factory.StartNew(() =>
                    {
                        using (canceller.Token.Register(Thread.CurrentThread.Abort))
                        {
                            return pCondition();
                        }
                    }, canceller.Token);
                }

                Thread.Sleep(sleepInterval);
            }

            canceller.Cancel();
            return result;
        }
    }
}
