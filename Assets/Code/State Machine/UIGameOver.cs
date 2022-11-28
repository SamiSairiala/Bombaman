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
            GameStateManager.Instance.Go(GameStateManager.Instance.PreviousState.Type);
        }


        
    }
}
