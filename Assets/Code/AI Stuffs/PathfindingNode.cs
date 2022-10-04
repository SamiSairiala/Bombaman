using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.XR;

namespace Bombaman.AI.Pathfinding
{
    public class PathfindingNode
    {
        public Vector2Int Location { get; private set; }
        public bool walkable { get; set; }
        public float G { get; private set; }
        public float H { get; private set;  }
        public float F { get { return this.G + this.H; } }
        public NodeState state { get; private set; }
        public PathfindingNode parentNode { get; set; }

    }
}
