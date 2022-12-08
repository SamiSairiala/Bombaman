using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.EventSystems;
using Bombaman.GameStates;
using UnityEngine.UI;


namespace Bombaman
{
	public class IntializePlayers : MonoBehaviour
	{

		public int PlayerCount = 0;

		[SerializeField] private TextMeshProUGUI text;

		public bool Ready = false;

		private InputDevice inputDevice;

		[SerializeField] private Sprite JoyStick;
		[SerializeField] private Sprite WASD;
		[SerializeField] private Sprite X;
		[SerializeField] private Sprite Square;
		[SerializeField] private Sprite E;
		[SerializeField] private Sprite Space;
		[SerializeField] private Sprite XXbox;
		[SerializeField] private Sprite A;
		

		[Header("Player1")]
		[SerializeField] private GameObject Player1;
		[SerializeField] private Image Movement1;
		[SerializeField] private Image Kick1;
		[SerializeField] private Image Bomb1;

		[Header("Player2")]
		[SerializeField] private GameObject Player2;
		[SerializeField] private Image Movement2;
		[SerializeField] private Image Kick2;
		[SerializeField] private Image Bomb2;

		[Header("Player3")]
		[SerializeField] private GameObject Player3;
		[SerializeField] private Image Movement3;
		[SerializeField] private Image Kick3;
		[SerializeField] private Image Bomb3;

		[Header("Player4")]
		[SerializeField] private GameObject Player4;
		[SerializeField] private Image Movement4;
		[SerializeField] private Image Kick4;
		[SerializeField] private Image Bomb4;


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



			var device = playerInput.devices[0]; // Gets players device.

			Debug.Log(device);

			

			PlayerCount++;
			text.text = ("Players joined: " + PlayerCount);
			#region Controller Showing
			if (PlayerCount == 1 && playerInput.gameObject.GetComponent<Character>().playerID == 1)
			{
				Player1.SetActive(true);
				if (device.name.Contains("DualShock"))
				{
					Movement1.sprite = JoyStick;
					Kick1.sprite = Square;
					Bomb1.sprite = X;
				}
				if (device.name.Contains("Keyboard"))
				{
					Movement1.sprite = WASD;
					Kick1.sprite = E;
					Bomb1.sprite = Space;
				}
				if (device.name.Contains("XInput"))
				{
					Movement1.sprite = JoyStick;
					Kick1.sprite = XXbox;
					Bomb1.sprite = A;
				}
			}
			if (PlayerCount == 2 && playerInput.gameObject.GetComponent<Character>().playerID == 2)
			{
				Player2.SetActive(true);
				if (device.name.Contains("Keyboard"))
				{
					Movement2.sprite = WASD;
					Kick2.sprite = E;
					Bomb2.sprite = Space;
				}
				if (device.name.Contains("DualShock"))
				{
					Movement2.sprite = JoyStick;
					Kick2.sprite = Square;
					Bomb2.sprite = X;
				}
				if (device.name.Contains("XInput"))
				{
					Movement2.sprite = JoyStick;
					Kick2.sprite = XXbox;
					Bomb2.sprite = A;
				}
			}
			if (PlayerCount == 3 && playerInput.gameObject.GetComponent<Character>().playerID == 3)
			{
				Player3.SetActive(true);
				if (device.name.Contains("DualShock"))
				{
					Movement3.sprite = JoyStick;
					Kick3.sprite = Square;
					Bomb3.sprite = X;
				}
				if (device.name.Contains("Keyboard"))
				{
					Movement3.sprite = WASD;
					Kick3.sprite = E;
					Bomb3.sprite = Space;
				}
				if (device.name.Contains("XInput"))
				{
					Movement3.sprite = JoyStick;
					Kick3.sprite = XXbox;
					Bomb3.sprite = A;
				}
			}
			if (PlayerCount == 4 && playerInput.gameObject.GetComponent<Character>().playerID == 4)
			{
				Player1.SetActive(true);
				if (device.name.Contains("DualShock"))
				{
					Movement4.sprite = JoyStick;
					Kick4.sprite = Square;
					Bomb4.sprite = X;
				}
				if (device.name.Contains("Keyboard"))
				{
					Movement4.sprite = WASD;
					Kick4.sprite = E;
					Bomb4.sprite = Space;
				}
				if (device.name.Contains("XInput"))
				{
					Movement4.sprite = JoyStick;
					Kick4.sprite = XXbox;
					Bomb4.sprite = A;
				}
			}
			#endregion

		}



		public void ReadyUp()
		{
			Debug.Log("Pressed start");
			Ready = true;

		}

		public void LobbyAndBack()
		{
			Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
			Destroy(this.gameObject);
			if (GameStateManager.Instance.CurrentState.Type == StateType.Lobby)
			{
				Destroy(GameObject.FindGameObjectWithTag("Player"));
				Destroy(GameObject.FindGameObjectWithTag("Player"));
				Destroy(GameObject.FindGameObjectWithTag("Player"));
				Destroy(GameObject.FindGameObjectWithTag("Player"));
				Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
			}


		}

		// Update is called once per frame
		void Update()
		{
			
			


			if (PlayerCount == 1 && Ready == true && SceneManager.GetActiveScene().name == "MainMenu")
			{
				//SceneManager.LoadScene("1Player");
				Destroy(GameObject.FindGameObjectWithTag("Player"));
				GameStateManager.Instance.Go(GameStates.StateType.InGame);// LOAD SINGLEPLAYER
			}
			if (PlayerCount == 2 && Ready == true && GameStateManager.Instance.CurrentState.Type == (StateType.Lobby))
			{
				Debug.Log("Going to 2 Player");
				/*SceneManager.LoadScene("2Player");*/ // LOAD 2 PLAYER SCENE.
				GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);
			}
			if (PlayerCount == 3 && Ready == true && GameStateManager.Instance.CurrentState.Type == (StateType.Lobby))
			{
				// LOAD 3 PLAYER
				GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);

			}
			if (PlayerCount == 4 && Ready == true && GameStateManager.Instance.CurrentState.Type == (StateType.Lobby))/* && SceneManager.GetActiveScene().name == "Lobby"*/
			{
				// LOAD 4 PLAYER
				GameStateManager.Instance.Go(GameStates.StateType.Multiplayer);

			}
		}
	}
}
