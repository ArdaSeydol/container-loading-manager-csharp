using System;

namespace ContainerManagement
{
    public abstract class Container
    {
        public string SerialNumber { get; }

        public double TareWeight { get; }

        public double MaxPayload { get; protected set; }

        public double CurrentCargoMass { get; protected set; }
        protected abstract char ContainerTypeCode { get; }

        protected Container(double tareWeight, double maxPayload)
        {
            TareWeight = tareWeight;
            MaxPayload = maxPayload;
            CurrentCargoMass = 0;

            int uniqueId = ContainerNumberGenerator.GetNewId();
            SerialNumber = $"KON-{ContainerTypeCode}-{uniqueId}";
        }
        public abstract void LoadCargo(double massToLoad, bool isHazardous);
        
        public virtual void UnloadCargo(double massToUnload)
        {
            if (massToUnload <= 0)
                return;

            CurrentCargoMass -= massToUnload;
            if (CurrentCargoMass < 0)
            {
                CurrentCargoMass = 0;
            }
        }

        public override string ToString()
        {
            return $"[{SerialNumber}] Tare={TareWeight} kg, MaxPayload={MaxPayload} kg, " +
                   $"CurrentCargo={CurrentCargoMass} kg";
        }
    }
}
