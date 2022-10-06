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
    public class Enemy : MonoBehaviour, IDamageable
    {

        private IMove mover;
        private LayerMask raycastLayers;
        private Vector3 levelOffset;
        [SerializeField] private Patrol patrol;

        private List<PathfindingNode> route;
        private PathfindingNode  nextNode;
        private Vector2 targetDir;

        [SerializeField] private Pathfinder pathfinder;
        private Grid grid;

        [SerializeField] private float Speed = 1;
        [SerializeField] private float MaxHealth = 1;

        public float Health { get; set; }

        private void Awake()
        {
            mover = GetComponent<IMove>();
            if (mover == null)
            {
                Debug.LogError("Can't find Mover (IMove interface");
            }
            mover.Setup(Speed);

            raycastLayers = LayerMask.GetMask();
            Health = MaxHealth;
            patrol = GetComponent<Patrol>();
            pathfinder = GetComponent<Pathfinder>();

            grid = FindObjectOfType<Grid>();
            if(grid == null) { Debug.Log("Can't find level"); }
            Tilemap tm = grid.GetComponentInChildren<Tilemap>();
            levelOffset = new Vector2(tm.cellBounds.xMin, tm.cellBounds.yMin);
            Debug.Log(tm.cellBounds);

            if (patrol == null) { Debug.LogError("Can't find Patrol"); }
            if (pathfinder == null) { Debug.LogError("Can't find Pathfinder"); }
            if (levelOffset == null) { Debug.LogError("Can't find level");}
        }

        private void FixedUpdate()
        {
            //target = checkForPlayer();
            if (route == null || route.Count == 0)
            {
                route = pathfinder.getRoute(pathfinder.GetPath(transform.position, patrol.getNextPoint()));
                nextNode = route.First();
                Debug.Log(nextNode.Location);
                targetDir = new Vector2(nextNode.Location.X, nextNode.Location.Y) -
                new Vector2(transform.position.x, transform.position.y);
                targetDir = targetDir.normalized;
            }
            if (Arrived(nextNode))
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

        private bool checkLineOfSight(GameObject target)
        {
            Vector2 targetDir = target.transform.position - transform.position;

            return checkDir(targetDir, 100).collider.IsTouchingLayers(0);
        }

        //checks direction for a wall within 1 unit
        private RaycastHit2D checkDir(Vector2 dir, int dist = 1)
        {
            RaycastHit2D raycast = Physics2D.Raycast(transform.position, dir, dist, raycastLayers);

            return raycast;
        }

        //Check for player
        private GameObject checkForPlayer()
        {
            if (checkDir(Vector2.up, 10).collider.IsTouchingLayers(0)) return checkDir(Vector2.up, 10).transform.gameObject;
            if (checkDir(Vector2.down, 10).collider.IsTouchingLayers(0)) return checkDir(Vector2.down, 10).transform.gameObject;
            if (checkDir(Vector2.left, 10).collider.IsTouchingLayers(0)) return checkDir(Vector2.left, 10).transform.gameObject;
            if (checkDir(Vector2.right, 10).collider.IsTouchingLayers(0)) return checkDir(Vector2.right, 10).transform.gameObject;

            return null;
            
        }

        private bool Arrived(PathfindingNode pf)
        {
            if (pf == null) return true;
            Vector2 curLoc = transform.position - new Vector3(pf.Location.X, pf.Location.Y);
            if (MathF.Abs(curLoc.x) < Speed*Time.deltaTime && MathF.Abs(curLoc.y) < Speed * Time.deltaTime)
            {
                Debug.Log("arrived");
                return true;
            }
            return false;
        }

        //checks if the unit can, within 1 frame, move to a space where x & y are both a whole number.
        private bool checkCentered()
        {
            float fixSpeed = Speed * Time.fixedDeltaTime;
            float fixX = Mathf.Abs(transform.position.x - Mathf.Floor(transform.position.x));
            float fixY = Mathf.Abs(transform.position.y - Mathf.Floor(transform.position.y));
            
            return fixX < fixSpeed && fixY < fixSpeed;
        }

        public void TakeDamage(float damageAmount)
        {
            Debug.Log("Enemy goes oof");
            this.Health -= damageAmount;
            if (this.Health < 0) Death();
        }

        public void Death()
        {
            Destroy(gameObject);
        }
    }
}
