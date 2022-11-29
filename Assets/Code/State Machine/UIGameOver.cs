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
        }

        public void Replay()
        {
            if(GameStateManager.Instance.PreviousState.Type == StateType.Multiplayer)
            {
                GameStateManager.Instance.Go(StateType.Lobby);
            } else
            {
                GameStateManager.Instance.Go(StateType.InGame);
            }
        }


        
    }
}
