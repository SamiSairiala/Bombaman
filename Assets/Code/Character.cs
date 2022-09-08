using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class Character : MonoBehaviour
    {
        // This script is for now only a place holder
        private Vector2 input;

        public PlayerInput playerInput;

        // Adds players input which is binded to "BOMB" to be called through variable.
        private InputAction bomb;

        private bool isDroppingBomb = false;

        [SerializeField] private GameObject bombPrefab;

        private Transform myTransform;

        // Start is called before the first frame update
        private void Start()
        {
            // Adds players input which is binded to "BOMB" to be called through variable.
            bomb = playerInput.actions["Bomb"];

            myTransform = gameObject.transform;
        }

        // Update is called once per frame
        private void Update()
        {
            transform.Translate(input * Time.deltaTime);
            if (bomb.WasPerformedThisFrame() && isDroppingBomb == false)
            {
                DropBomb();
            }
            if (bomb.WasPerformedThisFrame() && isDroppingBomb == true)
            {
                // Added a cooldown to how much bombs you can drop per second.
                Invoke("ChangeBombStatus", 1f);
            }
        }

        public void Move(InputAction.CallbackContext context)
        {
            // Reads the users input using inputsystem callback.
            input = context.ReadValue<Vector2>();
        }

        // Added a cooldown to how much bombs you can drop per second.
        private void ChangeBombStatus()
        {
            isDroppingBomb = false;
        }

        public void DropBomb()
        {
            if (bombPrefab)
            {
                Instantiate(bombPrefab, new Vector3(Mathf.RoundToInt(myTransform.position.x), Mathf.RoundToInt(myTransform.position.y), bombPrefab.transform.position.y), bombPrefab.transform.rotation);
                isDroppingBomb = true;
            }
        }
    }
}