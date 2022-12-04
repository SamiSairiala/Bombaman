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
		#region PlayerInputs
		private InputAction bomb;

        private InputAction kick;

        private InputAction pause;
		#endregion
		private IMove mover;

        #region Bools
        public bool kicking = false;

        private bool Player1Alive = true;
        private bool Player2Alive = true;
		#endregion

		float animationKickDuration = 1f;

        [SerializeField]
        private int playerIndex = 0;

        private CharacterController controller;

        public Vector2 startPosition;

        public int playerID; // For multiplayer

        private Transform myTransform;

        public float Speed; // Run speed

        private AudioSource audioSource;

        private AudioSteps audioStepScript;

        [SerializeField] private AudioClip Kicking;

        public float Health { get; set; }

        [SerializeField] private float health = 1f;

        [SerializeField] private BombController bombController;

        private bool Loaded = false;

        [Header("Animations")]
        [SerializeField] private Animator animator;
        [SerializeField] private AnimationClip KickingClip;


        private GameSystem gameSystem;

        public PauseMenu pauseMenu;

        public RigidbodyMover RBMover;
        private bool isReady = false;

        [SerializeField] private RuntimeAnimatorController AnimController1;
        [SerializeField] private RuntimeAnimatorController AnimController2;



        // Start is called before the first frame update
        private void Start()
        {
            RBMover = GetComponent<RigidbodyMover>();
            // Adds players input which is binded to "BOMB" to be called through variable.
            bomb = playerInput.actions["Bomb"];

            mover.Setup(Speed);

            myTransform = gameObject.transform;

            Health = health;

            kick = playerInput.actions["Kick"];

            pause = playerInput.actions["Pause"];

            transform.position = startPosition;

            audioSource = GetComponent<AudioSource>();

            audioStepScript = GetComponent<AudioSteps>();

            animator = GetComponent<Animator>();
            isReady = true;

            if(SceneManager.GetActiveScene().name == "level1")
			{
                transform.position = GameObject.Find("Spawn1").transform.position;
                startPosition = GameObject.FindGameObjectWithTag("Spawn1").transform.position;
            }
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

            if(SceneManager.GetActiveScene().name == "Lobby")
			{
                DontDestroyOnLoad(this.gameObject);
            }
            if (SceneManager.GetActiveScene().name == "MainMenu")
            {
                Destroy(this.gameObject);
            }

            #region PauseMenu
            if ((SceneManager.GetActiveScene().name.ToLower().StartsWith("level") || SceneManager.GetActiveScene().name == "2Player"))
			{
                
                pauseMenu = FindObjectOfType<PauseMenu>();
                if (pause.WasPerformedThisFrame())
                {
                    pauseMenu = FindObjectOfType<PauseMenu>();
                    pauseMenu.PauseUnPause();
			    }
                if(pauseMenu.Paused == true)
			    {
                    bombController.enabled = false;
			    }
			    if(pauseMenu.Paused == false)
			    {
                    bombController.enabled = true;
                }
            }
            #endregion

            if (SceneManager.GetActiveScene().name == "2Player" && Loaded == false)
            {
                Loaded = true;
                SpawnPlayers();
                bombController.enabled = true; // Enables bombcontroller which is disabled in mainmenu so cant drop bombs in mainmenu.
                gameSystem = FindObjectOfType<GameSystem>();
                pauseMenu = FindObjectOfType<PauseMenu>();
            }
            if (SceneManager.GetActiveScene().name == "level0" && Loaded == false) //Singleplayer
            {
                Loaded = true;
                pauseMenu = FindObjectOfType<PauseMenu>();
                gameSystem = FindObjectOfType<GameSystem>();
                transform.position = GameObject.Find("Spawn1").transform.position;
                startPosition = GameObject.FindGameObjectWithTag("Spawn1").transform.position;
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
                transform.position = GameObject.Find("Spawn2").transform.position; 
                startPosition = GameObject.FindGameObjectWithTag("Spawn2").transform.position;
                animator.runtimeAnimatorController = AnimController2;
                
            }
            if (playerID == 3)
            {
                transform.position = GameObject.Find("Spawn3").transform.position;
                startPosition = GameObject.FindGameObjectWithTag("Spawn3").transform.position;

            }
            if (playerID == 4)
            {
                transform.position = GameObject.Find("Spawn4").transform.position;
                startPosition = GameObject.FindGameObjectWithTag("Spawn4").transform.position;

            }
        }

        private void Kick()
        {
            if (!kicking)
            {
                animator.SetBool("Kicking", true);
                audioSource.PlayOneShot(Kicking);
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
            if (!isReady) return;

            // Reads the users input using inputsystem callback.
            input = context.ReadValue<Vector2>();
            if (RBMover.GridMovement == false)
            {
                mover.Move(input);
                audioStepScript.PlaySteps();
            }


			if (RBMover.GridMovement == true)
			{

				if (RBMover.isMoving == false)
				{
                    input = transform.position + new Vector3(input.x, input.y, 0);
					StartCoroutine(RBMover.MovePlayerGrid(input));
				}
			}

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

            if(SceneManager.GetActiveScene().name.ToLower().StartsWith("level"))
			{
                gameSystem = FindObjectOfType<GameSystem>();
                gameSystem.Player1Alive = false;
            }

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
                if (playerID == 3)
                {
                    gameSystem.Player3Alive = false;
                }
                if (playerID == 4)
                {
                    gameSystem.Player4Alive = false;
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
