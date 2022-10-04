using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

namespace Bombaman
{
    public interface IDamageable
    {
        float Health { get; set; }

        void TakeDamage(float damageAmount);

        void Death();


    }
}
