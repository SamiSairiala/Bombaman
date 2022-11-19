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
        PathfindingNode currentPath;
        Map map;

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

        public void Awake()
        {
            Grid grid = FindObjectOfType<Grid>();
            Tilemap[] temp = grid.GetComponentsInChildren<Tilemap>();
            map = grid.GetComponent<Map>();
        }

        public PathfindingNode GetPath(Vector2 start, Vector2 end)
        {
            return GetPath(new Point(Mathf.RoundToInt(start.x), Mathf.RoundToInt(start.y)), new Point(Mathf.RoundToInt(end.x), Mathf.RoundToInt(end.y)));
        }

        public PathfindingNode GetPath(Point start, Point end)
        {
            
            
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
                var adjacentTiles = GetWalkableAdjacentTiles(current.Location.X, current.Location.Y);
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

        #region Internal stuff
        /// <summary>
        /// Returns list of PathfindingNodes next to node at x y, that can be walked upon
        /// </summary>
        /// <param name="x">X of the tile the list is adjacent to</param>
        /// <param name="y">Y of the tile the list is adjacent to</param>
        /// <param name="map">Boolean map of obstacles</param>
        /// <returns></returns>
        private List<PathfindingNode> GetWalkableAdjacentTiles(int x, int y)
        {
            List<PathfindingNode> result = new List<PathfindingNode>()
            {
                new PathfindingNode(x+1, y),
                new PathfindingNode(x-1, y),
                new PathfindingNode(x, y+1),
                new PathfindingNode(x, y-1)
            };

            Debug.Log("x: " + x + ", y: " + y);

            return result.Where(l => !map.IsWalkableOffset(l.Location.X, l.Location.Y)).ToList();
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

        #endregion
    }
}
