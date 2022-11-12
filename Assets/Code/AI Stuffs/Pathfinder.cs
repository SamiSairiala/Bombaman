using Bombaman.AI.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;
using Color = UnityEngine.Color;

namespace Bombaman
{
    public class Pathfinder : MonoBehaviour
    {
        private bool[,] walkable;
        Vector2Int offset;
        PathfindingNode currentPath;

        public List<PathfindingNode> getRoute(PathfindingNode path)
        {
            List<PathfindingNode> temp = new List<PathfindingNode>();
            while(path.parentNode != null)
            {
                temp.Add(path);
                path = path.parentNode;
            }

            temp.Reverse();

            return temp;
        }

        public PathfindingNode GetPath(Vector2 start, Vector2 end)
        {
            return GetPath(new Point(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y)), new Point(Mathf.RoundToInt(end.x), Mathf.RoundToInt(end.y)));
        }

        public PathfindingNode GetPath(Point start, Point end)
        {
            UpdateOffset();
            
            PathfindingNode current = null;
            PathfindingNode startPoint = new PathfindingNode(start);
            PathfindingNode endPoint = new PathfindingNode(end);
            List<PathfindingNode> openList = new List<PathfindingNode>();
            List<PathfindingNode> closedList = new List<PathfindingNode>();
            int g = 0;

            openList.Add(startPoint);
            while(openList.Count > 0)
            {
                var lowest = openList.Min(l => l.F);
                current = openList.First(l => l.F == lowest);
                
                closedList.Add(current);
                openList.Remove(current);


                //if target point is in closed list, path found
                if (closedList.FirstOrDefault(l => l.Location.X == endPoint.Location.X &&
                        l.Location.Y == endPoint.Location.Y) != null)
                {
                    break;
                }
                var adjacentTiles = GetWalkableAdjacentTiles(current.Location.X, current.Location.Y, GetWalkable());
                g++;

                foreach(var tile in adjacentTiles)
                {
                    if(closedList.FirstOrDefault(l => l.Location.X == tile.Location.X && l.Location.Y == tile.Location.Y) == null)
                    {
                        tile.G = g;
                        tile.H = ComputeH(tile.Location, endPoint.Location);
                        tile.parentNode = current;

                        openList.Insert(0, tile);
                    } else
                    {
                        if(g+tile.H < tile.F)
                        {
                            tile.G = g;
                            tile.parentNode = current;
                        }
                    }
                }
            }
            currentPath = current;
            return currentPath;
        }

        #region Gizmo
        private void OnDrawGizmosSelected()
        {
            if (walkable == null) updatedTerrain();
            UpdateOffset();
            PathfindingNode tempPath = currentPath;
            Vector3 tempLoc;
            if(tempPath != null)
            {
                Gizmos.color = Color.green;
                while(tempPath != null)
                {
                    tempLoc = new Vector3(tempPath.Location.X, tempPath.Location.Y);
                    Gizmos.DrawSphere(tempLoc, 0.1f);
                    tempPath = tempPath.parentNode;
                    Gizmos.color = Color.yellow;
                }
            }

            Gizmos.color = Color.black;


            for(int x = 0; x<walkable.GetLength(0); x++)
            {
                for (int y = 0; y<walkable.GetLength(1); y++)
                {
                    if (walkable[x, y]) { 
                        Gizmos.DrawCube(new Vector3(x - offset.x, y - offset.y), new Vector3(0.2f,0.2f));
                    }
                }
            }
        }
        #endregion

        #region Internal stuff
        /// <summary>
        /// Returns list of PathfindingNodes next to node at x y, that can be walked upon
        /// </summary>
        /// <param name="x">X of the tile the list is adjacent to</param>
        /// <param name="y">Y of the tile the list is adjacent to</param>
        /// <param name="map">Boolean map of obstacles</param>
        /// <returns></returns>
        private List<PathfindingNode> GetWalkableAdjacentTiles(int x, int y, bool[,] map)
        {
            List<PathfindingNode> result = new List<PathfindingNode>()
            {
                new PathfindingNode(x+1, y),
                new PathfindingNode(x-1, y),
                new PathfindingNode(x, y+1),
                new PathfindingNode(x, y-1)
            };

            return result.Where(l => !map[l.Location.X + offset.x, l.Location.Y + offset.y]).ToList();
        }

        private void UpdateOffset()
        {
            walkable = updatedTerrain();

            Grid grid = FindObjectOfType<Grid>();
            offset = new Vector2Int(walkable.GetLength(0), walkable.GetLength(1));
            offset /= 2;
            offset -= new Vector2Int(Mathf.RoundToInt(grid.transform.position.x), Mathf.RoundToInt(grid.transform.position.y));
            offset.x -= 1;
        }

        /// <summary>
        /// Calculates the fastest possible route to target, ignoring obstacles.
        /// </summary>
        /// <param name="start"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private int ComputeH(Point start, Point target)
        {
            return Mathf.Abs(target.X - start.X) + Mathf.Abs(target.Y - start.Y);
        }


        /// <summary>
        /// If walkable terrain has not been checked, checks it
        /// </summary>
        /// <returns>Returns walkable terrain as bool array where true = obstacle</returns>
        private bool[,] GetWalkable()
        {
            if (walkable == null)
            {
                updatedTerrain();
            }
            return walkable;
        }

        /// <summary>
        /// Finds grid type map on the scene, converts the tiles to a boolean array of walkable squares
        /// </summary>
        /// <returns>Boolean array of walkable tiles, where TRUE means there is an obstacle</returns>
        private bool[,] updatedTerrain()
        {
            Grid grid = FindObjectOfType<Grid>();
            Tilemap[] tilemap;
            tilemap = grid.GetComponentsInChildren<Tilemap>();

            tilemap[0].CompressBounds();
            
            Vector2Int[] cellOffset = new Vector2Int[tilemap.Length];
            walkable = new bool[tilemap[0].cellBounds.size.x, tilemap[0].cellBounds.size.y];
            for (int i = 0; i < tilemap.Length - 1; i++)
            {
                var bounds = tilemap[i].cellBounds;
                cellOffset[i] = new Vector2Int(Mathf.Abs(bounds.xMin), Mathf.Abs(bounds.yMin));
                for (int y = bounds.size.y-1; y >= 0; y--)
                {
                    for (int x = 0; x < bounds.size.x; x++)
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
            return walkable;
        }
        #endregion
    }
}
