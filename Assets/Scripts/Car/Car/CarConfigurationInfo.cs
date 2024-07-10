using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Vehicle.Car
{
    public static class CarConfigurationInfo
    {
        public static CarConfiguration CarConfiguration = new CarConfiguration()
        {
            MinRPM = 800,
            MaxRPM = 7000,
            MaxSpeed = 180,
            TopGear = 6,
            HorsePower = 600 
        };

        public static float AverageGearSpeed => CarConfiguration.MaxSpeed / CarConfiguration.TopGear;
    }
}