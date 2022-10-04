using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Pathfinder : MonoBehaviour
    {
        bool[,] walkable;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {

        }

        private void Awake()
        {
            updateTerrain();
        }

        //Updates the walkable bool array according to the tilemaps within grid object in the scene.
        //TRUE = unpassable, FALSE = passable
        private void updateTerrain()
        {
            Grid grid = FindObjectOfType<Grid>();
            Tilemap[] tilemap;
            tilemap = grid.GetComponentsInChildren<Tilemap>();
            foreach (Tilemap tm in tilemap)
            {
                tm.CompressBounds();
            }
            Vector2Int[] cellOffset = new Vector2Int[tilemap.Length];
            walkable = new bool[tilemap[0].cellBounds.size.x, tilemap[0].cellBounds.size.y];
            for (int i = 0; i < tilemap.Length; i++)
            {
                var bounds = tilemap[i].cellBounds;
                cellOffset[i] = new Vector2Int(Mathf.Abs(bounds.xMin), Mathf.Abs(bounds.yMin));
                for (int x = 0; x < bounds.size.x; x++)
                {
                    for (int y = 0; y < bounds.size.y; y++)
                    {
                        var xoff = cellOffset[0].x - cellOffset[i].x;
                        var yoff = cellOffset[0].y - cellOffset[i].y;
                        var px = bounds.xMin + x;
                        var py = bounds.yMin + y;

                        if (walkable[x + xoff, y + yoff]) continue;

                        walkable[x + xoff, y + yoff] = tilemap[i].HasTile(new Vector3Int(px, py, 0));

                    }
                }
            }
        }
    }
}
