using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;


namespace Bombaman
{
    public class IntializePlayers : MonoBehaviour
    {

        public int PlayerCount = 0;

        [SerializeField] private TextMeshProUGUI text;

        public bool Ready = false;

        private InputDevice inputDevice;


        //[SerializeField] private GameObject Mainmenu;

        //[SerializeField] private GameObject menuFirstButton;



        // Start is called before the first frame update
        void Start()
        {
			//EventSystem.current.SetSelectedGameObject(null);
			//EventSystem.current.SetSelectedGameObject(menuFirstButton);
		}


        void OnPlayerJoined(PlayerInput playerInput)
        {
           

            Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
            // Set the player ID, add one to the index to start at Player 1
            playerInput.gameObject.GetComponent<Character>().playerID = playerInput.playerIndex + 1; // Player 1 = 0, Player 2 = 1 etc.

            if (SceneManager.GetActiveScene().name == "Lobby")
            {
                text.text = ("Player " + playerInput.playerIndex + " Joined");
            }
           

            PlayerCount++;

            
        }

        

        public void ReadyUp()
		{
            Debug.Log("Pressed start");
            Ready = true;
            if (PlayerCount == 2 && Ready == true && SceneManager.GetActiveScene().name == "Lobby")
            {
                SceneManager.LoadScene("2Player"); // LOAD 2 PLAYER SCENE.
                                                   //GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);
            }
        }

        public void LobbyAndBack()
		{
            Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
            Destroy(this.gameObject);
            if(SceneManager.GetActiveScene().name == "Lobby")
			{
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
            }
            
		}

        // Update is called once per frame
        void Update()
        {
            if(PlayerCount == 1 && Ready == true && SceneManager.GetActiveScene().name == "MainMenu")
			{
                //SceneManager.LoadScene("1Player");
                GameStateManager.Instance.Go(GameStates.StateType.InGame);// LOAD SINGLEPLAYER
            }
            if (PlayerCount == 2 && Ready == true && SceneManager.GetActiveScene().name == "Lobby") 
            {
				SceneManager.LoadScene("2Player"); // LOAD 2 PLAYER SCENE.
				//GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);
            }
            if (PlayerCount == 3 && Ready == true && SceneManager.GetActiveScene().name == "Lobby")
            {
                // LOAD 3 PLAYER

            }
            if (PlayerCount == 4 && Ready == true && SceneManager.GetActiveScene().name == "Lobby")
            {
                // LOAD 4 PLAYER

            }
        }
    }
}
