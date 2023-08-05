namespace Interfaces
{
    using System;
    using Events;

    public interface IProgressable
    {
        public event EventHandler<OnProgressChangedEventArgs> OnProgressChanged;
    }
}
