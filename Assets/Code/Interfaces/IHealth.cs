using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public interface IHealth
    {
        int CurrentHealth { get; }
        int MaxHealth { get; }
        int MinHealth { get { return 0; } }
        void IncreaseHealth(int amount);
        bool DecreseHealth(int amount);
        void Reset();
    }
}
