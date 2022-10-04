using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace Bombaman
{
    public class Enemy : MonoBehaviour
    {

        private IMove mover;
        private LayerMask raycastLayers;
        private Vector2 currentDirection;
        private Vector2 previousDirection;

        [SerializeField] private float Speed = 1;


        GameObject target;

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            mover = GetComponent<IMove>();
            if(mover == null)
            {
                Debug.LogError("Can't find Mover (IMove interface");
            }

            raycastLayers = LayerMask.GetMask();

        }

        private void FixedUpdate()
        {
            target = checkForPlayer();

            if(target == null)
            {

            } else
            {
                if (checkLineOfSight(target))
                {

                }
            }
        }

        private bool checkLineOfSight(GameObject target)
        {
            Vector2 targetDir = target.transform.position - transform.position;

            return checkDir(targetDir, 100).collider.IsTouchingLayers(0);
        }

        //checks which directions the enemy can move at least 1 unit towards without hitting a wall
        private List<Vector2> validDirections()
        {
            List<Vector2> directions = new List<Vector2>();

            if (checkDir(Vector2.up).collider) directions.Add(Vector2.up);
            if (checkDir(Vector2.down).collider) directions.Add(Vector2.down);
            if (checkDir(Vector2.left).collider) directions.Add(Vector2.left);
            if (checkDir(Vector2.right).collider) directions.Add(Vector2.right);


            return directions;
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

        //checks if the unit can, within 1 frame, move to a space where x & y are both a whole number.
        private bool checkCentered()
        {
            float fixSpeed = Speed * Time.fixedDeltaTime;
            float fixX = Mathf.Abs(transform.position.x - Mathf.Floor(transform.position.x));
            float fixY = Mathf.Abs(transform.position.y - Mathf.Floor(transform.position.y));
            
            return fixX < fixSpeed && fixY < fixSpeed;
        }
    }
}
