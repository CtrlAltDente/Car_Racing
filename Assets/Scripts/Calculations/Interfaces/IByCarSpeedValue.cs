using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Cars_Racing.Calculations.Interfaces
{
    public interface IByCarSpeedValue<T>
    {
        T ByCarSpeedValue { get; }
    }
}