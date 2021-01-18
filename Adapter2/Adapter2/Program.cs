using System;

namespace Adapter2
{
    class Program
    {
        static void Main(string[] args)
        {
            ISensor sensor = new SpeedAdapterToKmPerHour(new MilesPerHourSensor());
            Console.WriteLine(sensor.GetSpeed());
        }

        class MilesPerHourSensor
        {
            public double GetSpeedInMilesPerHour()
            {
                var rand = new Random();
                return rand.NextDouble() * 100;
            }
        }

        interface ISensor
        {
            public double GetSpeed();
        }

        class SpeedAdapterToKmPerHour : ISensor
        {
            private MilesPerHourSensor sensor;

            public SpeedAdapterToKmPerHour(MilesPerHourSensor sensor)
            {
                this.sensor = sensor;
            }

            public double GetSpeed()
            {
                return sensor.GetSpeedInMilesPerHour() * 1.60934;
            }
        }
    }
}
