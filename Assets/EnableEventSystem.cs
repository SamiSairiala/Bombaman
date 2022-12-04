using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class EnableEventSystem : MonoBehaviour
    {
		[SerializeField] private GameObject eventSystem;
		private bool BacktoMenu = false;
        // Start is called before the first frame update
        void Start()
        {
            
        }

		public void BackToMenuFrom()
		{
			BacktoMenu = true;
		}

		// Update is called once per frame

		private void Update()
		{
			if (GameStateManager.Instance.CurrentState.Type == GameStates.StateType.MainMenu)
			{
				Debug.Log("Enabling");
				eventSystem.SetActive(true);
			}
			
		}


		public void EnableMenuEvents()
		{
            
		}
    }
}
