using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class RigidbodyMover : MonoBehaviour, IMove
    {
        private Rigidbody2D rigidbody;

        private Vector2 Movement;

        [SerializeField]private Animator animator;

        public float Speed
        {
            get;
            private set;
        }

        public void Move(Vector2 direction)
        {
            Movement = direction * Speed;
            if(gameObject.tag == "Player")
			{
                animator = GetComponent<Animator>();
                animator.SetFloat("Horizontal", Movement.x);
                animator.SetFloat("Vertical", Movement.y);
                animator.SetFloat("Speed", Movement.sqrMagnitude);
                
            }
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
            if (gameObject.tag == "Player")
            {
                animator = GetComponent<Animator>();
                
            }
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
