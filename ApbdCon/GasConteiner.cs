using System;

namespace ContainerManagement
{
    public class GasContainer : Container, IHazardNotifier
    {
        protected override char ContainerTypeCode => 'G';
        public double Pressure { get; }

        public GasContainer(double tareWeight, double maxPayload, double pressure)
            : base(tareWeight, maxPayload)
        {
            Pressure = pressure;
        }

        public override void LoadCargo(double massToLoad, bool isHazardous)
        {
            if (massToLoad <= 0) return;

            double allowed = isHazardous ? 0.5 * MaxPayload : 0.9 * MaxPayload;

            double potentialNewMass = CurrentCargoMass + massToLoad;

            if (potentialNewMass > allowed)
            {
                NotifyHazard(SerialNumber,
                    $"Attempted to load {massToLoad} kg (hazard={isHazardous}) which exceeds allowed capacity {allowed} kg.");
                throw new OverfillException($"Cannot load beyond {allowed} kg. Hazardous = {isHazardous}");
            }

            CurrentCargoMass = potentialNewMass;
        }

        public override void UnloadCargo(double massToUnload)
        {
           
            base.UnloadCargo(massToUnload);

           
            double leftover = CurrentCargoMass * 0.05;
            if (CurrentCargoMass > leftover)
            {
                CurrentCargoMass = leftover;
            }
        }

        public void NotifyHazard(string containerNumber, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[HAZARD] Gas Container {containerNumber}: {message}");
            Console.ResetColor();
        }
    }
}