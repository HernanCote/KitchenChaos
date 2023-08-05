namespace Events
{
    using System;
    using Enums;

    public class OnStateChangedEventArgs : EventArgs
    {
        public State State;
    }
}