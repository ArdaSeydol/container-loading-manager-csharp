using System;

namespace ContainerManagement
{
    public static class ContainerNumberGenerator
    {
        private static int _nextId = 1;
        
        public static int GetNewId()
        {
            return _nextId++;
        }
    }
}