using System;

namespace ContainerManagement
{
    class Program
    {
        static void Main(string[] args)
        {
            ContainerShip ship1 = new ContainerShip("Atlantis", maxSpeed: 20, maxContainerCount: 5, maxWeightTons: 60);
            ContainerShip ship2 = new ContainerShip("Neptune", maxSpeed: 25, maxContainerCount: 5, maxWeightTons: 50);
            
            Container liquid1 = new LiquidContainer(tareWeight: 3000, maxPayload: 12000); // 12,000 kg capacity
            Container gas1 = new GasContainer(tareWeight: 2000, maxPayload: 10000, pressure: 5.0);
            Container reefer1 = new RefrigeratedContainer(tareWeight: 4000, maxPayload: 10000, 
                                                          initialTemperature: 5, requiredTemperature: 2);

            // Load cargo into containers
            try
            {
                liquid1.LoadCargo(5000, isHazardous: true);
                Console.WriteLine($"{liquid1.SerialNumber} loaded with 5000 kg hazardous cargo.");
                
                gas1.LoadCargo(6000, isHazardous: false);
                Console.WriteLine($"{gas1.SerialNumber} loaded with 6000 kg non-hazardous gas.");
                
                reefer1.LoadCargo(8000, isHazardous: false);
                Console.WriteLine($"{reefer1.SerialNumber} loaded with 8000 kg of cargo.");
            }
            catch (OverfillException ex)
            {
                Console.WriteLine($"Exception: {ex.Message}");
            }

            ship1.LoadContainer(liquid1);
            ship1.LoadContainer(gas1);
            ship1.LoadContainer(reefer1);
            
            ship1.PrintInfo();
            
            ContainerShip.TransferContainer(gas1.SerialNumber, ship1, ship2);
            
            Console.WriteLine("After transfer:");
            ship1.PrintInfo();
            ship2.PrintInfo();
            
            gas1.UnloadCargo(3000);
            Console.WriteLine($"{gas1.SerialNumber} after unloading 3000 kg. Current cargo: {gas1.CurrentCargoMass} kg");
            
            if (reefer1 is RefrigeratedContainer rc)
            {
                rc.SetTemperature(-5); // Attempt below required => clamp
                Console.WriteLine($"{rc.SerialNumber} temperature set to {rc.Temperature}°C " +
                                  $"(required min: {rc.RequiredTemperature}°C)");
            }

            Console.WriteLine("\nPress any key to exit...");
            Console.ReadKey();
        }
    }
}

