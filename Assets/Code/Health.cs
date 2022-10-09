using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class Health : MonoBehaviour, IHealth
    {
        [SerializeField]
        private int minHealth = 0;
        [SerializeField]
        public int maxHealth = 10;
        [SerializeField]
        private int startHealth = 10;
        private int currentHealth;
        public int CurrentHealth { get { return currentHealth; } private set { currentHealth = Mathf.Clamp(value, minHealth, maxHealth); } } 

        public int MaxHealth { get { return maxHealth; } }
        public int MinHealth { get { return minHealth; } }

        public bool DecreseHealth(int amount)
        {
            if (amount < 0) return currentHealth > minHealth;
            CurrentHealth -= amount;
            return currentHealth > minHealth;
        }

        public void IncreaseHealth(int amount)
        {
            if(amount < 0) return;
            CurrentHealth += amount;
        }

        public void Reset()
        {
            CurrentHealth = startHealth;
        }
    }
}
