using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class UIOptions : MonoBehaviour
    {
        public void QuitGame()
        {
            GameStateManager.Instance.Go(StateType.MainMenu);
            
        }
        public void Back()
        {
            GameStateManager.Instance.GoBack();
        }
    }
}
