using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
        public float Damage = 1f;

       public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.gameObject.tag == "Player")
            {
                Character player = FindObjectOfType<Character>();
                player.TakeDamage(Damage);
                
                
            }
            if(collision.gameObject.tag == "Enemy") // For AI or remember to change this tag to what we call ai's
            {

            }
        }

    }
}
