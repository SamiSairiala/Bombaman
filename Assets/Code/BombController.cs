using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEngine.Tilemaps;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class BombController : MonoBehaviour
    {
        public LayerMask explosionLayerMask;

        public PlayerInput playerInput;
        private InputAction bomb;
        private Transform myTransform;
        private bool isDroppingBomb = false;

        [Header("Explosion")]
        [SerializeField] private Explosion explosionPrefab; // The prefab must have explosion script attached to it.
        [SerializeField] float explosionDuration = 1f; // How long the explosion lasts.
        [SerializeField] int explosionRadius = 0; // How big the explosion or how many tiles it spreads in tiles.

        [Header("Destructible")]
        [SerializeField] private Tilemap BreakableTiles;
        [SerializeField] private Destructible destructiblePrefab;

        [SerializeField] private GameObject BombPrefab;

        public float BombFuse = 3f;
        // Start is called before the first frame update
        void Start()
        {
            // Adds players input which is binded to "BOMB" to be called through variable.
            bomb = playerInput.actions["Bomb"];
            
            myTransform = gameObject.transform;
        }

        // Update is called once per frame
        void Update()
        {
            if (bomb.WasPerformedThisFrame() && isDroppingBomb == false)
            {
                StartCoroutine(PlaceBomb());
            }
            if (bomb.WasPerformedThisFrame() && isDroppingBomb == true)
            {
                // Added a cooldown to how much bombs you can drop per second.
                Invoke("ChangeBombStatus", 1f);
            }
        }

        private IEnumerator PlaceBomb()
        {
            Vector2 position = transform.position;
            position = transform.position;

            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            // Snaps bombs to "grid" and also spawns them
            GameObject bomb = Instantiate(BombPrefab, position, Quaternion.identity);

            yield return new WaitForSeconds(3f); // How long till bomb explodes.

            position = bomb.transform.position;
            position.x = Mathf.Round(position.x);
            position.y = Mathf.Round(position.y);

            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            Destroy(explosion.gameObject, explosionDuration);
            explosion.DestroyAfter(explosionDuration); // Destroy explosion prefab.
            Explode(position, Vector2.up, explosionRadius); // Directions in which to spawn explosions.
            Explode(position, Vector2.down, explosionRadius);
            Explode(position, Vector2.left, explosionRadius);
            Explode(position, Vector2.right, explosionRadius);

            Destroy(bomb.gameObject);
        }

        /// <summary>
        /// Checks if hits destructibles etc. also Goes through if tiles are empty so can spawn bombs.
        /// </summary>
        /// <param name="position">where to check eg. from explosion gameobject checks if touches certain layers.</param>
        /// <param name="direction">Mainly when we start animating this will be used</param>
        /// <param name="lenght">how big is the radius of the bomb eg. 1 is 1 tile in all directions.</param>
        private void Explode(Vector2 position, Vector2 direction, int lenght)
        {
            if(lenght <= 0)
            {
                return;
            }

            position += direction;

            if(Physics2D.OverlapBox(position, Vector2.one / 2f, 0f, explosionLayerMask))
            {
                ClearDestructible(position);
                return;
            }

            Explosion explosion = Instantiate(explosionPrefab, position, Quaternion.identity);
            
            Destroy(explosion.gameObject, explosionDuration);
            explosion.DestroyAfter(explosionDuration);

            Explode(position, direction, lenght - 1);
        }
        private void OnTriggerExit2D(Collider2D ExitBomb)
        {
            if (ExitBomb.gameObject.layer == LayerMask.NameToLayer("Bomb"))
            {
                ExitBomb.isTrigger = false;
            }
        }

        private void ClearDestructible(Vector2 position)
        {
            Vector3Int cell = BreakableTiles.WorldToCell(position);
            TileBase tile = BreakableTiles.GetTile(cell);

            if(tile != null)
            {
                BreakableTiles.SetTile(cell, null); // Clears the tile bomb hits.
            }
        }


        private void ChangeBombStatus()
        {
            isDroppingBomb = false;
        }

    }
}
