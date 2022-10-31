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
        [SerializeField] private Tile PowerUpTile;

		private void Awake()
		{
            Grid grid = FindObjectOfType<Grid>();
            Tilemap[] tilemap = grid.GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tm in tilemap)
            {
                if (tm.gameObject.layer == 10) // Change this if breakable tiles order changes in layers.
                {
                    BreakableTiles = tm;
                }
            }
            grid = FindObjectOfType<Grid>();
        }

		
        public void SpawnPowerup(Vector2 position)
        {
            int randomRange = Random.Range(1, 101);
            if (randomRange <= 50)
            {
                Debug.Log("Spawning");
                
                //Vector3Int cell = BreakableTiles.WorldToCell(position);
                //TileBase tile = BreakableTiles.GetTile(cell);

                //position.x = cell.x;
                //position.y = cell.y;

                

                //BreakableTiles.SetTile(cell, null); // Clears the tile bomb hits.
                //position = new Vector2(cell.x, cell.y);
                //Debug.Log(position);
                Instantiate(powerup, position, Quaternion.identity);
            }
            return;
        }
    }
}
