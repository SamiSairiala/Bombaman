using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
        public int Damage = 10;

       public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            IHealth target = collision.GetComponent<IHealth>();
            if (target != null)
            {

           
            IDamageable damageable = collision.GetComponent<IDamageable>();
            if (!target.DecreseHealth(Damage))
            {
                damageable.Death();
                
                
            }
            }

        }

    }
}
