using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;


namespace Bombaman
{
    public class IntializePlayers : MonoBehaviour
    {

        private int PlayerCount = 0;

        [SerializeField] private TextMeshProUGUI text;

        private bool Ready = false;

        private InputDevice inputDevice;

        
        // Start is called before the first frame update
        void Start()
        {
        
        }


        void OnPlayerJoined(PlayerInput playerInput)
        {
           

            Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
            // Set the player ID, add one to the index to start at Player 1
            playerInput.gameObject.GetComponent<Character>().playerID = playerInput.playerIndex + 1; // Player 1 = 0, Player 2 = 1 etc.

            text.text = ("Player " + playerInput.playerIndex  + " Joined");

           

            PlayerCount++;

            
        }

        

        public void ReadyUp()
		{
            Ready = true;
		}

        // Update is called once per frame
        void Update()
        {
            if(PlayerCount == 1 && Ready == true)
			{
                //SceneManager.LoadScene("1Player");
                GameStateManager.Instance.Go(GameStates.StateType.InGame);// LOAD SINGLEPLAYER
            }
            if (PlayerCount == 2 && Ready == true) 
            {
				SceneManager.LoadScene("2Player"); // LOAD 2 PLAYER SCENE.
				//GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);
            }
            if (PlayerCount == 3 && Ready == true)
            {
                // LOAD 3 PLAYER

            }
            if (PlayerCount == 4 && Ready == true)
            {
                // LOAD 4 PLAYER

            }
        }
    }
}
