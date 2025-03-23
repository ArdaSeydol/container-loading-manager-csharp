using System;

namespace ContainerManagement
{
    public interface IHazardNotifier
    {
        void NotifyHazard(string containerNumber, string message);
    }
}