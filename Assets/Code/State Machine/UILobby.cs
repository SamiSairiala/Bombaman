using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class UILobby : MonoBehaviour
    {
        public void Back()
        {
            GameStateManager.Instance.GoBack();
        }

        public void StartGame()
        {
            GameStateManager.Instance.Go(StateType.Multiplayer);
        }
    }
}
