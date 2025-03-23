using System;

namespace ContainerManagement
{
    public enum ProductType
    {
        Bananas,
        Milk,
        Helium,
        Fuel
        // etc.
    }

    public class RefrigeratedContainer : Container
    {
        protected override char ContainerTypeCode => 'C';

        public ProductType? StoredProductType { get; private set; }

        public double Temperature { get; private set; }

        public double RequiredTemperature { get; }

        public RefrigeratedContainer(double tareWeight, double maxPayload, 
                                     double initialTemperature, double requiredTemperature)
            : base(tareWeight, maxPayload)
        {
            Temperature = initialTemperature;
            RequiredTemperature = requiredTemperature;
        }

        public override void LoadCargo(double massToLoad, bool isHazardous)
        {
            if (massToLoad <= 0) return;

            if (isHazardous)
            {
                throw new OverfillException("Refrigerated container doesn't support hazardous cargo.");
            }

            double allowed = 0.9 * MaxPayload;

            double potentialNewMass = CurrentCargoMass + massToLoad;
            if (potentialNewMass > allowed)
            {
                throw new OverfillException($"Cannot load beyond {allowed} kg into a refrigerated container.");
            }

            CurrentCargoMass = potentialNewMass;
        }
        
        public void SetTemperature(double newTemperature)
        {
            if (newTemperature < RequiredTemperature)
            {
                Console.WriteLine($"Warning: Attempt to set temperature below required minimum of {RequiredTemperature}°C.");
                // For demonstration, we’ll clamp it:
                Temperature = RequiredTemperature;
            }
            else
            {
                Temperature = newTemperature;
            }
        }
    }
}
