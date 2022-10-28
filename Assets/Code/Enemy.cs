using Bombaman.AI;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Bombaman
{
    public class Enemy : EnemyBase
    {

        [SerializeField] private Patrol patrol;

        protected override void Awake()
        {
            patrol = GetComponent<Patrol>();
            if (patrol == null) { Debug.LogError("Can't find Patrol"); }

            base.Awake();
        }

        // Update is called once per frame
        protected override void FixedUpdate()
        {
            if (base.route == null || base.route.Count == 0)
            {
                base.route = base.pathfinder.getRoute(base.pathfinder.GetPath(transform.position, patrol.getNextPoint()));
                base.nextNode = base.route.First();
                Debug.Log(nextNode.Location);
                base.targetDir = new Vector2(nextNode.Location.X, nextNode.Location.Y) -
                new Vector2(transform.position.x, transform.position.y);
                base.targetDir = base.targetDir.normalized;
            }

            base.FixedUpdate();
        }
    }
}
