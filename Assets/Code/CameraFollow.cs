using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class CameraFollow : MonoBehaviour
    {
        [SerializeField] private Transform target;  // Target which camera will follow.

        [SerializeField] private float snaptoTargetTime = 0.2f; // How smoothly it follows.

        private Vector3 velocity;
        private float zOffset;

        private void Start()
        {
            Camera camera = GetComponent<Camera>();
            if(camera != null)
            {
                zOffset = camera.transform.localPosition.z;
            }
        }

        private void Update()
        {
            if(target != null)
            {

           
            Vector3 targetPosition = target.TransformPoint(new Vector3(0, 0, zOffset));
            transform.position = Vector3.SmoothDamp(transform.position, targetPosition, ref velocity, snaptoTargetTime);
            }
        }
    }
}
