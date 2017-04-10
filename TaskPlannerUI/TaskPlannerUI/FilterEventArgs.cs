using System;

namespace TaskPlannerUI
{
    public class FilterEventArgs : EventArgs
    {
        public string Filter { get;  }

        public FilterEventArgs(string filter)
        {
            Filter = filter;
        }
    }
}