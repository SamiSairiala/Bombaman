using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class PowerupPickup : MonoBehaviour
    {

        private SpriteRenderer spriteRend;

        [SerializeField] private Sprite BlastRadiusSprite;
        [SerializeField] private Sprite SpeedIncreaseSprite;
        [SerializeField] private Sprite MaxBombsSprite;

		private void Start()
		{
            
		}

		public enum ItemType // Powerup types
        {
            BlastRadius,
            SpeedIncrease,
            MaxBombsIncrease,
        }


        public ItemType type; // Here to select which one the gameobject is from editor.

        private void Awake()
        {
            spriteRend = GetComponent<SpriteRenderer>();
            int random = Random.Range(0, 3);
            if (random == 0)
            {
                type = ItemType.BlastRadius;
                spriteRend.sprite = BlastRadiusSprite;
            }
            if(random == 1)
            {
                type = ItemType.SpeedIncrease;
                spriteRend.sprite = SpeedIncreaseSprite;
            }
            if(random == 2)
			{
                type = ItemType.MaxBombsIncrease;
                spriteRend.sprite = MaxBombsSprite;
            }

        }

        private void OnItemPickup(GameObject player)
        {
            
            switch (type)
            {
                case ItemType.BlastRadius:
                    player.GetComponent<BombController>().explosionRadius++; // If player pickups blastradius powerup increase explosion radius by 1.
                    break;

                case ItemType.SpeedIncrease:
                    player.GetComponent<Character>().Speed += 0.5f;
                    break;

                case ItemType.MaxBombsIncrease:
                    player.GetComponent<BombController>().MaxBombs++;
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
