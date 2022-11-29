using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class UIGameOver : MonoBehaviour
    {
        public void BackToMenu()
        {
            GameStateManager.Instance.Go(StateType.MainMenu);
            Destroy(GameObject.FindGameObjectWithTag("Player"));
            Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
        }

        public void Replay()
        {
            if(GameStateManager.Instance.PreviousState.Type == StateType.Multiplayer)
            {
                GameStateManager.Instance.Go(StateType.Lobby);
                Destroy(GameObject.FindGameObjectWithTag("Player"));
                Destroy(GameObject.FindGameObjectWithTag("EventSystem"));
            } else
            {
                GameStateManager.Instance.Go(StateType.InGame);
            }
        }


        
    }
}
