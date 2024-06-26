using System;
using System.Runtime.InteropServices;
using System.Threading;

namespace VanityApp
{
    internal static class Program
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern ExecutionState SetThreadExecutionState(ExecutionState esFlags);

        [Flags]
        private enum ExecutionState : uint
        {
            ES_AWAYMODE_REQUIRED = 0x00000040,
            ES_CONTINUOUS = 0x80000000,
            ES_DISPLAY_REQUIRED = 0x00000002,
            ES_SYSTEM_REQUIRED = 0x00000001
        }

        private static void Main()
        {
            using AutoResetEvent autoResetEvent = new AutoResetEvent(false);
            using Timer timer = new Timer(state => SetThreadExecutionState(ExecutionState.ES_AWAYMODE_REQUIRED | ExecutionState.ES_CONTINUOUS | ExecutionState.ES_DISPLAY_REQUIRED | ExecutionState.ES_SYSTEM_REQUIRED), autoResetEvent, 0, -1);
            autoResetEvent.WaitOne();
        }
    }
}