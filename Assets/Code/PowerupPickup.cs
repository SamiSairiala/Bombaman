using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class PowerupPickup : MonoBehaviour
    {
        public enum ItemType // Powerup types
        {
            BlastRadius,
            SpeedIncrease,
        }


        public ItemType type; // Here to select which one the gameobject is from editor.

        private void OnItemPickup(GameObject player)
        {
            switch (type)
            {
                case ItemType.BlastRadius:
                    player.GetComponent<BombController>().explosionRadius++; // If player pickups blastradius powerup increase explosion radius by 1.
                    break;

                case ItemType.SpeedIncrease:
                    player.GetComponent<Character>().Speed++;
                    break;
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.CompareTag("Player"))
            {
                OnItemPickup(collision.gameObject);
                Destroy(gameObject); // Destroy the gameobject after player has picked it up.
            }
        }
    }
}
