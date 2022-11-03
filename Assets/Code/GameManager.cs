using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class GameManager : MonoBehaviour
    {
        public List<PlayerInput> playerList = new List<PlayerInput>();

        [SerializeField] private InputAction joinAction;
        [SerializeField] private InputAction LeaveAction;

        public static GameManager instance = null;

        public event System.Action<PlayerInput> PlayerJoinedGame;
        public event System.Action<PlayerInput> PlayerLeftGame;

		// Start is called before the first frame update
		private void Awake()
		{
			if(instance == null)
			{
                instance = this;
			}
            else if(instance != null)
			{
                Destroy(gameObject);
			}

			joinAction.Enable();
			joinAction.performed += context => JoinAction(context);

		}


		private void Start()
		{
			PlayerInputManager.instance.JoinPlayer(0, -1, null);
		}

		void OnPlayerJoined(PlayerInput playerInput)
		{
			playerList.Add(playerInput);

			if(PlayerJoinedGame != null)
			{
				PlayerJoinedGame(playerInput);
			}
		}

		void OnPlayerLeft(PlayerInput playerInput)
		{

		}

		void JoinAction(InputAction.CallbackContext context)
		{

		}
		
    }
}
