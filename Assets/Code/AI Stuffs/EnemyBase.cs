using Bombaman.AI;
using Bombaman.AI.Pathfinding;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Runtime.CompilerServices;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public abstract class EnemyBase : MonoBehaviour, IDamageable
    {

        private IMove mover;
        private LayerMask raycastLayers;
        private Vector3 levelOffset;

        protected List<PathfindingNode> route;
        protected PathfindingNode  nextNode;
        protected Vector2 targetDir;

        [SerializeField] protected Pathfinder pathfinder;
        private Grid grid;

        [SerializeField] private float Speed = 1;

        public Health health;

        public float Health { get; set; }

        protected virtual void Awake()
        {
            mover = GetComponent<IMove>();
            if (mover == null)
            {
                Debug.LogError("Can't find Mover (IMove interface");
            }
            mover.Setup(Speed);

            raycastLayers = LayerMask.GetMask();
            health = GetComponent<Health>();
            pathfinder = GetComponent<Pathfinder>();

            grid = FindObjectOfType<Grid>();
            if(grid == null) { Debug.Log("Can't find level"); }
            Tilemap tm = grid.GetComponentInChildren<Tilemap>();
            levelOffset = new Vector2(tm.cellBounds.xMin, tm.cellBounds.yMin);
            Debug.Log(tm.cellBounds);

            if (pathfinder == null) { Debug.LogError("Can't find Pathfinder"); }
            if (levelOffset == null) { Debug.LogError("Can't find level");}
        }

        protected virtual void FixedUpdate()
        {
            //target = checkForPlayer();
            if (Arrived(nextNode) && route != null && route.Count != 0)
            {
                route.Remove(nextNode);
                if(route.Count != 0)
                {
                    nextNode = route.First();
                    targetDir = new Vector2(nextNode.Location.X, nextNode.Location.Y) -
                    new Vector2(transform.position.x, transform.position.y);
                    targetDir = targetDir.normalized;
                }
            }

            mover.Move(targetDir);
            //if (target == null)
            //{

            //} else
            //{
            //    if (checkLineOfSight(target))
            //    {

            //    }
            //}
        }

        protected virtual bool Arrived(PathfindingNode pf)
        {
            if (pf == null) return true;
            Vector2 curLoc = transform.position - new Vector3(pf.Location.X, pf.Location.Y);
            if (MathF.Abs(curLoc.x) < Speed*Time.deltaTime && MathF.Abs(curLoc.y) < Speed * Time.deltaTime)
            {
                return true;
            }
            return false;
        }

        //checks if the unit can, within 1 frame, move to a space where x & y are both a whole number.
        protected virtual bool checkCentered()
        {
            float fixSpeed = Speed * Time.fixedDeltaTime;
            float fixX = Mathf.Abs(transform.position.x - Mathf.Floor(transform.position.x));
            float fixY = Mathf.Abs(transform.position.y - Mathf.Floor(transform.position.y));
            
            return fixX < fixSpeed && fixY < fixSpeed;
        }

        public virtual void TakeDamage(float damageAmount)
        {
            Debug.Log("Enemy goes oof for: " + damageAmount + "dmg");
            if (health.DecreseHealth(Mathf.RoundToInt(damageAmount))) Death();
        }

        public virtual void Death()
        {
            Destroy(gameObject);
        }
    }
}
