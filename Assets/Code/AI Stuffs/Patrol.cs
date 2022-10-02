using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman.AI
{
    public class Patrol : MonoBehaviour
    {
        [SerializeField]
        private PatrolType patrolType = PatrolType.Iterate; //Iterate goes 0->1->2->1, bounce goes 0->1->2->1->0->1...

        [SerializeField]
        private Vector2[] patrolPoints;

        private int i = -1;

        private void Start()
        {
            
        }

        private void Awake()
        {
            RoundPath();
        }

        public Vector2 getNextPoint()
        {
            i++;

            if (patrolType == PatrolType.Iterate)
            {
                return patrolPoints[i % patrolPoints.Length];
            } else
            {
                return patrolPoints[pos()];
            }
        }

        //draw gizmo for patrol
        private void OnDrawGizmosSelected()
        {
            Gizmos.color = Color.red;
            RoundPath();
            if (patrolPoints != null)
            {
                foreach (Vector2 point in patrolPoints)
                {
                    Vector2 tempPoint = point;

                    if (point != null)
                    {
                        tempPoint.x = Mathf.Round(point.x);
                        tempPoint.y = Mathf.Round(point.y);

                        Gizmos.DrawSphere(tempPoint, .25f);
                    }
                }
            }
            DrawPath();
        }

        //draw path
        private void DrawPath()
        {
            Gizmos.color = Color.red;
            Vector2 thisPoint = patrolPoints[0];
            for (int j = 0; j < (patrolPoints.Length-1)*2; j++)
            {
                Vector2 nextPoint = getNextPoint();
                Gizmos.DrawLine(thisPoint, nextPoint);
                thisPoint = nextPoint;
            }
            
        }

        private int pos()
        {
            int range = (int) patrolPoints.Length - 1;

            return (int)Mathf.Abs(((i + range) % (range * 2)) - range);
        }

        private void RoundPath()
        {
            //round the patrol points
            if (patrolPoints != null)
            {
                Vector2[] tempPoints = new Vector2[patrolPoints.Length];
                int j = 0;
                foreach (Vector2 point in patrolPoints)
                {
                    Vector2 tempPoint;
                    tempPoint.x = Mathf.Round(point.x);
                    tempPoint.y = Mathf.Round(point.y);
                    tempPoints[j] = tempPoint;

                    j++;
                }
                patrolPoints = tempPoints;
            }
        }
    }
}
