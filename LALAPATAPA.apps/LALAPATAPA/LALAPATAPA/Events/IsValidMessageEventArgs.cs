using System;

namespace LALAPATAPA.Events
{
    public class IsValidMessageEventArgs : EventArgs
    {
        public IsValidMessageEventArgs(bool isValid)
        {
            IsValid = isValid;
        }

        public bool IsValid { get; }
    }
}
