using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class LobbyState : GameStateBase
    {
        public override string SceneName
        {
            get { return "Lobby"; }
        }

        public override StateType Type
        {
            get { return StateType.Lobby; }
        }

        //public override bool IsAdditive
        //{
        //    get { return true; }
        //}

        public LobbyState() : base()
        {
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.Multiplayer);
        }
    }
}
