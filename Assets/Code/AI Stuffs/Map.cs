using Bombaman.AI.Pathfinding;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Map : MonoBehaviour
    {

        private bool[,] walkable;
        private bool[,] wallLocation;

        public Vector2 Offset { get; private set; }

        Vector2Int bounds;
        private Tilemap breakable, unbreakable;

        // Start is called before the first frame update
        void Awake()
        {
            Tilemap[] temp = GetComponentsInChildren<Tilemap>();
            breakable = temp[0];
            unbreakable = temp[1];
            bounds = new Vector2Int(Mathf.Max(temp[0].size.x, temp[1].size.x),
                Mathf.Max(temp[0].size.y, temp[1].size.y));
            Debug.Log("bounds: " + bounds);
            wallLocation = GetWalkable(bounds);

            Offset = new Vector2(Mathf.Min(breakable.cellBounds.xMin, unbreakable.cellBounds.xMin),
                Mathf.Min(breakable.cellBounds.yMin, unbreakable.cellBounds.yMin));

        }
            
        private bool[,] GetWalkable(Vector2Int bounds)
        {
            bool[,] temp = new bool[bounds.x, bounds.y];

            for (int x = 0; x < bounds.x; x++)
            {
                for (int y = 0; y < bounds.y; y++)
                {
                    temp[x, y] = (breakable.HasTile(new Vector3Int(x + Mathf.RoundToInt(Offset.x), y + Mathf.RoundToInt(Offset.y))) || 
                        unbreakable.HasTile(new Vector3Int(x + Mathf.RoundToInt(Offset.x), y + Mathf.RoundToInt(Offset.y))));
                }
            }

            return temp;
        }
        private bool[,] GetWalkable()
        {
            if(bounds == null)
            {
                Tilemap[] temp = GetComponentsInChildren<Tilemap>();
                breakable = temp[0];
                unbreakable = temp[1];
                bounds = new Vector2Int(Mathf.Max(temp[0].size.x, temp[1].size.x),
                    Mathf.Max(temp[0].size.y, temp[1].size.y));
            }

            return GetWalkable(bounds);
        }

        public bool IsWalkable(float x, float y)
        {
            if (walkable == null) walkable = GetWalkable();
            Debug.Log("x: " + x + ", y: " + y);
            return walkable[Mathf.RoundToInt(x-1), Mathf.RoundToInt(y-1)];
        }

        public bool IsWalkableOffset(float x, float y)
        {
            if (Offset == null)
            {
                Offset = new Vector2(Mathf.Min(breakable.cellBounds.xMin, unbreakable.cellBounds.xMin),
                Mathf.Min(breakable.cellBounds.yMin, unbreakable.cellBounds.yMin));
            }

            return IsWalkable(Mathf.Abs(x - Offset.x), Mathf.Abs(y - Offset.y));
        }

        #region Gizmo
        private void OnDrawGizmosSelected()
        {
            Offset = new Vector2(Mathf.Min(breakable.cellBounds.xMin, unbreakable.cellBounds.xMin),
                Mathf.Min(breakable.cellBounds.yMin, unbreakable.cellBounds.yMin));

            Gizmos.color = Color.black;
            Tilemap[] temp = GetComponentsInChildren<Tilemap>();
            breakable = temp[0];
            unbreakable = temp[1];

            if (walkable == null) walkable = GetWalkable(new Vector2Int(Mathf.Max(temp[0].size.x, temp[1].size.x),
                Mathf.Max(temp[0].size.y, temp[1].size.y)));

            for (int x = 0; x < walkable.GetLength(0); x++)
            {
                for (int y = 0; y < walkable.GetLength(1); y++)
                {
                    if (walkable[x, y])
                    {
                        Gizmos.DrawCube(new Vector3(x + Offset.x + 1, y + Offset.y + 1), new Vector3(0.2f, 0.2f));
                    }
                }
            }
        }
        #endregion
    }

}
