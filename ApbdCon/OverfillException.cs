using System;

namespace ContainerManagement
{
    public class OverfillException : Exception
    {
        public OverfillException(string message) : base(message) {}
    }
}