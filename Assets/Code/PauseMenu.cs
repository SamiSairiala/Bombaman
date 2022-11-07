using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class PauseMenu : MonoBehaviour
    {
        [SerializeField] private GameObject pauseMenu, optionsMenu;

        [SerializeField] private GameObject pauseFirstButton, optionsFirstButton, optionsClosedButton;

       

		

		public bool Paused = false;

		private void Start()
		{
			
		}



		private void Update()
		{
			
		}


		public void PauseUnPause()
		{
			if (!pauseMenu.activeInHierarchy)
			{
				Paused = true;
                pauseMenu.SetActive(true);
                Time.timeScale = 0;

                EventSystem.current.SetSelectedGameObject(null);
                EventSystem.current.SetSelectedGameObject(pauseFirstButton);

			}
			else
			{
				Paused = false;
                pauseMenu.SetActive(false);
                Time.timeScale = 1;
                optionsMenu.SetActive(false);
			}
		}
    }
}
