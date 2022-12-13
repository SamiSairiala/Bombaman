using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class CreditsScene : MonoBehaviour
    {
        [SerializeField] private GameObject eventSystem;
        // Start is called before the first frame update
        void Start()
        {
        
        }

        // Update is called once per frame
        private void Update()
        {
            if (GameStateManager.Instance.CurrentState.Type == GameStates.StateType.Credits)
            {
                Debug.Log("Enabling");
                eventSystem.SetActive(true);
            }

        }

        public void Menu()
		{
            GameStateManager.Instance.Go(GameStates.StateType.MainMenu);
        }
    }
}
