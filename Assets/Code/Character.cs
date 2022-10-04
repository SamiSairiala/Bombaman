using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class Character : MonoBehaviour, IDamageable
    {
        // This script is for now only a place holder
        private Vector2 input;

        public PlayerInput playerInput;

        // Adds players input which is binded to "BOMB" to be called through variable.
        private InputAction bomb;

        

        private IMove mover;

        

        private Transform myTransform;

        [SerializeField] private float Speed;

        public float Health { get; set; }

        [SerializeField] private float health = 1f;

        // Start is called before the first frame update
        private void Start()
        {
            // Adds players input which is binded to "BOMB" to be called through variable.
            bomb = playerInput.actions["Bomb"];

            mover.Setup(Speed);

            myTransform = gameObject.transform;

            Health = health;
        }

        private void Awake()
        {
            mover = GetComponent<IMove>();
            if (mover == null)
            {
                Debug.LogError("Cant find a component which implements the IMove interface!");
            }
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Translate(input * Time.deltaTime);
            
        }

        public void Move(InputAction.CallbackContext context)
        {
            // Reads the users input using inputsystem callback.
            input = context.ReadValue<Vector2>();
            mover.Move(input);
        }

        public void TakeDamage(float damageAmount)
        {
            health -= damageAmount;
            if(health == 0)
            {
                Death();
            }
            
        }

        
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion"))
            {
                float damage = FindObjectOfType<Explosion>().Damage;
                TakeDamage(damage);
            }
        }

        public void Death()
        {
            enabled = false;
            GetComponent<BombController>().enabled = false;
            Destroy(gameObject);
        }



        //public void DropBomb(GameObject BombPrefab, Transform DroppersTransform)
        //{

        //    if (bombPrefab)
        //    {
        //        // Snaps bombs to "grid" and also spawns them.
        //        Instantiate(BombPrefab, new Vector3(Mathf.RoundToInt(DroppersTransform.position.x), Mathf.RoundToInt(DroppersTransform.position.y), BombPrefab.transform.position.y), BombPrefab.transform.rotation);
        //        // Adds a true boolean to stop players from dropping multiple bombs per frame.
        //        isDroppingBomb = true;
        //    }
        //}



        // Added a cooldown to how much bombs you can drop per second.



        //public void DropBomb()
        //{
        //    if (bombPrefab)
        //    {
        //        // Snaps bombs to "grid" and also spawns them.
        //        Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y), bombPrefab.transform.position.y), bombPrefab.transform.rotation);
        //        // Adds a true boolean to stop players from dropping multiple bombs per frame.
        //        isDroppingBomb = true;
        //    }
        //}
    }
}
