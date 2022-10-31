using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Bombaman
{
    public class SpawnPowerups : MonoBehaviour
    {

        [SerializeField] private GameObject powerup;
        private int randomRange;
        [SerializeField] private Tilemap BreakableTiles;
        [SerializeField] private Destructible destructiblePrefab;

        private void Awake()
        {
           
            int randomRange = Random.Range(1, 101);
            Grid grid = FindObjectOfType<Grid>();
            Tilemap[] tilemap = grid.GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tm in tilemap)
            {
                if (tm.gameObject.layer == 10)
                {
                    BreakableTiles = tm;
                }
            }
            grid = FindObjectOfType<Grid>();
        }
        public void SpawnPowerup(Vector2 position)
        {
            if (randomRange < 50)
            {
                
                Vector3Int cell = BreakableTiles.WorldToCell(position);
                TileBase tile = BreakableTiles.GetTile(cell);

                
                if (tile != null)
                {
                    Instantiate(powerup, transform); // TODO:
                }
            }
        }
    }
}
