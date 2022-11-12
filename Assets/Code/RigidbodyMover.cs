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

        public bool GridMovement = false;

        public bool isMoving;
        private Vector3 origPos, targetPos;

        
        public float Speed
        {
            get;
            private set;
        }

        public void Move(Vector2 direction)
        {
           
            Movement = direction * Speed;
            
            if (gameObject.tag == "Player")
			{
                animator = GetComponent<Animator>();
                animator.SetFloat("Horizontal", Movement.x);
                animator.SetFloat("Vertical", Movement.y);
                animator.SetFloat("Speed", Movement.sqrMagnitude);
                
            }
        }

        public IEnumerator MovePlayerGrid(Vector3 direction)
		{
            isMoving = true;
   //         float elapsedTime = 0;
   //         origPos = transform.position;
   //         targetPos = origPos + direction;

   //         while(elapsedTime < Speed)
			//{
   //             transform.position = Vector3.Lerp(origPos, targetPos, (elapsedTime / Speed));
   //             elapsedTime += Time.deltaTime;
   //             yield return null;
			//}
   //         transform.position = targetPos;

            while((direction - transform.position).sqrMagnitude > Mathf.Epsilon)
			{
                transform.position = Vector3.MoveTowards(transform.position, direction, Speed * Time.fixedDeltaTime - 0.5f);
                yield return null;
			}
            transform.position = direction;
            isMoving = false;
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
