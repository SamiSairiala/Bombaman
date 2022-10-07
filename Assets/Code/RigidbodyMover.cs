using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class RigidbodyMover : MonoBehaviour, IMove
    {
        private Rigidbody2D rigidbody;

        private Vector2 Movement;

        public float Speed
        {
            get;
            private set;
        }

        public void Move(Vector2 direction)
        {
            Movement = direction * Speed;
        }

        public void Setup(float speed)
        {
            Speed = speed;
        }

        private void FixedUpdate()
        {
            
            rigidbody.velocity = new Vector2(Movement.x * Speed , Movement.y * Speed);
        }

        // Start is called before the first frame update
        void Start()
        {
        
        }

        private void Awake()
        {
            rigidbody = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
        
        }
    }
}
