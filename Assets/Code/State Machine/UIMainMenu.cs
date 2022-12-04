using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class UIMainMenu : MonoBehaviour
    {
        public void StartGame()
        {
            GameStateManager.Instance.Go(StateType.InGame);
        }

        public void Lobby()
        {
            GameStateManager.Instance.Go(StateType.Lobby);
           
        }

        public void Options()
        {
            GameStateManager.Instance.Go(StateType.Options);
            GameObject.FindGameObjectWithTag("EventSystem").SetActive(false);
        }

        public void QuitGame()
        {
            Application.Quit();
        }

    }
}
