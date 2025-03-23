using System;

namespace ContainerManagement
{
    public class LiquidContainer : Container, IHazardNotifier
    {
        protected override char ContainerTypeCode => 'L';

        public LiquidContainer(double tareWeight, double maxPayload)
            : base(tareWeight, maxPayload)
        {
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

        public void NotifyHazard(string containerNumber, string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"[HAZARD] Liquid Container {containerNumber}: {message}");
            Console.ResetColor();
        }
    }
}