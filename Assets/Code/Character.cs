using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class Character : MonoBehaviour
    {
        // This script is for now only a place holder
        private Vector2 input;
        // Start is called before the first frame update
        void Start()
        {

        }

        // Update is called once per frame
        void Update()
        {
            transform.Translate(input * Time.deltaTime);
        }


        public void Move(InputAction.CallbackContext context)
        {
            // Reads the users input using inputsystem callback.
            input = context.ReadValue<Vector2>();
        }
    }
}
