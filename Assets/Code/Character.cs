using JetBrains.Annotations;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using UnityEngine.Animations;

namespace Bombaman
{
    public class Character : MonoBehaviour, IDamageable
    {
        // This script is for now only a place holder
        private Vector2 input;

        public PlayerInput playerInput;

        // Adds players input which is binded to "BOMB" to be called through variable.
        private InputAction bomb;

        private InputAction kick;

        private IMove mover;

        public bool kicking = false;

        private bool Player1Alive = true;
        private bool Player2Alive = true;

        float animationKickDuration = 1f;

        [SerializeField]
        private int playerIndex = 0;

        private CharacterController controller;

        public Vector2 startPosition;

        public int playerID; // For multiplayer

        private Transform myTransform;

        public float Speed; // Run speed

        public float Health { get; set; }

        [SerializeField] private float health = 1f;

        [SerializeField] private BombController bombController;

        private bool Loaded = false;

        [Header("Animations")]
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip KickingClip;

        private GameSystem gameSystem;

        // Start is called before the first frame update
        private void Start()
        {
            // Adds players input which is binded to "BOMB" to be called through variable.
            bomb = playerInput.actions["Bomb"];

            mover.Setup(Speed);

            myTransform = gameObject.transform;

            Health = health;

            kick = playerInput.actions["Kick"];

            transform.position = startPosition;

           

            animator = GetComponent<Animator>();
        }

        public int GetPlayerIndex()
        {
            return playerIndex;
        }

        private void Awake()
        {
            
            mover = GetComponent<IMove>();
            if (mover == null)
            {
                Debug.LogError("Cant find a component which implements the IMove interface!");
            }
            controller = GetComponent<CharacterController>();
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Translate(input * Time.deltaTime);
            if (kick.WasPerformedThisFrame()) // This is only here for the time being
            {
                 Kick();
            }
            mover.Setup(Speed);

            if(SceneManager.GetActiveScene().name == "MainMenu")
			{
                DontDestroyOnLoad(this.gameObject);
            }
            


            if (SceneManager.GetActiveScene().name == "2Player" && Loaded == false)
            {
                Loaded = true;
                SpawnPlayers();
                bombController.enabled = true;
                gameSystem = FindObjectOfType<GameSystem>();
            }
            if (SceneManager.GetActiveScene().name == "1Player" && Loaded == false) //Singleplayer
            {
                Loaded = true;
                
                bombController.enabled = true;
            }
        }


        public void SpawnPlayers()
		{
            Debug.Log("Spawning");
            if(playerID == 1)
			{
                transform.position = GameObject.Find("Spawn1").transform.position;
                startPosition = GameObject.FindGameObjectWithTag("Spawn1").transform.position;
            }
            if(playerID == 2)
			{
                transform.position = GameObject.Find("Spawn2").transform.position; // TODO: FIX THESE
                startPosition = GameObject.FindGameObjectWithTag("Spawn2").transform.position;
            }
        }

        private void Kick()
        {
            if (!kicking)
            {
                animator.SetBool("Kicking", true);
                kicking = true;
                Invoke("StopKicking", animationKickDuration);
            }
        }

        private void StopKicking()
        {
            animator.SetBool("Kicking", false);
            kicking = false;
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
            if(health <= 0)
            {
                Death();
            }
            
        }

        
        

        public void Death()
        {
            // TODO: Declare winner in somewhere for multiplayer
            enabled = false;
            GetComponent<BombController>().enabled = false;
            Debug.Log("Player " + playerID + " Died!");

            if (SceneManager.GetActiveScene().name == "2Player")
            {
                if(playerID == 1)
				{
                    gameSystem.Player1Alive = false;
				}
                if(playerID == 2)
				{
                    gameSystem.Player2Alive = false;
				}
            }

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
