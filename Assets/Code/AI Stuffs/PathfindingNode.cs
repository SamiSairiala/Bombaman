using System.Collections;
using System.Collections.Generic;
using System.Drawing;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

namespace Bombaman.AI.Pathfinding
{
    public class PathfindingNode
    {
        public Point Location;
        public int G;
        public int H;
        public int F { get { return this.G + this.H; } }
        public PathfindingNode parentNode { get; set; }

        public PathfindingNode(int x, int y)
        {
            this.Location = new Point(x, y);
        }
        public PathfindingNode(Point p)
        {
            this.Location = p;
        }

    }
}
