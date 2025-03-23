using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerManagement
{
    public class ContainerShip
    {
        public string Name { get; }
        public double MaxSpeed { get; }
        public int MaxContainerCount { get; }
        public double MaxWeightTons { get; }
        
       
        private readonly List<Container> _containers = new List<Container>();

        public ContainerShip(string name, double maxSpeed, int maxContainerCount, double maxWeightTons)
        {
            Name = name;
            MaxSpeed = maxSpeed;
            MaxContainerCount = maxContainerCount;
            MaxWeightTons = maxWeightTons;
        }

        public double CurrentTotalWeightKg
        {
            get
            {
                return _containers.Sum(c => c.TareWeight + c.CurrentCargoMass);
            }
        }

        public bool LoadContainer(Container container)
        {
            if (_containers.Count >= MaxContainerCount)
            {
                Console.WriteLine($"Cannot load container {container.SerialNumber}: container capacity reached.");
                return false;
            }

            double totalWeightWithNew = CurrentTotalWeightKg + container.TareWeight + container.CurrentCargoMass;
            double maxWeightKg = MaxWeightTons * 1000; // convert tons -> kg
            if (totalWeightWithNew > maxWeightKg)
            {
                Console.WriteLine($"Cannot load container {container.SerialNumber}: weight limit exceeded.");
                return false;
            }

            _containers.Add(container);
            Console.WriteLine($"Loaded container {container.SerialNumber} onto ship {Name}.");
            return true;
        }

        public bool UnloadContainer(string serialNumber)
        {
            var container = _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                Console.WriteLine($"No container with serial {serialNumber} found on ship {Name}.");
                return false;
            }

            _containers.Remove(container);
            Console.WriteLine($"Unloaded container {serialNumber} from ship {Name}.");
            return true;
        }

        public bool ReplaceContainer(string oldSerialNumber, Container newContainer)
        {
            // remove old
            var removed = UnloadContainer(oldSerialNumber);
            if (!removed)
            {
                Console.WriteLine($"Replacement failed. Old container not found or not removed.");
                return false;
            }
            // try load new
            return LoadContainer(newContainer);
        }

        public static bool TransferContainer(string serialNumber, ContainerShip fromShip, ContainerShip toShip)
        {
            // find container
            var container = fromShip._containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
            if (container == null)
            {
                Console.WriteLine($"Container {serialNumber} not found on ship {fromShip.Name}.");
                return false;
            }

            bool canLoad = toShip.LoadContainer(container);
            if (!canLoad)
            {
                Console.WriteLine($"Transfer failed: Could not load container {serialNumber} onto {toShip.Name}.");
                return false;
            }
            fromShip._containers.Remove(container);
            Console.WriteLine($"Transferred container {serialNumber} from {fromShip.Name} to {toShip.Name}.");
            return true;
        }

        public Container FindContainer(string serialNumber)
        {
            return _containers.FirstOrDefault(c => c.SerialNumber == serialNumber);
        }

        public void PrintInfo()
        {
            Console.WriteLine($"Ship '{Name}'");
            Console.WriteLine($"  Max Speed: {MaxSpeed} knots");
            Console.WriteLine($"  Max Containers: {MaxContainerCount}");
            Console.WriteLine($"  Max Weight: {MaxWeightTons} tons");
            Console.WriteLine($"  Current Onboard Weight (kg): {CurrentTotalWeightKg}\n");
            Console.WriteLine("Containers on board:");
            foreach (var c in _containers)
            {
                Console.WriteLine($"  - {c}");
            }
            Console.WriteLine();
        }
    }
}
