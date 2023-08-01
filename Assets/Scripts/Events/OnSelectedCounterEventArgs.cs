using System;

namespace Events
{
    using Counters;

    public class OnSelectedCounterEventArgs : EventArgs
    {
        public BaseCounter SelectedCounter;
    }
}
