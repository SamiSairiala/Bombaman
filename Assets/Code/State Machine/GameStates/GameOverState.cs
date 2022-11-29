using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class GameOverState : GameStateBase
    {
        public override string SceneName { get { return "GameOver"; } }

        public override StateType Type { get { return StateType.GameOver; } }

        //public override bool IsAdditive
        //{
        //    get { return true; }
        //}

        public GameOverState() : base()
        {
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.Lobby);
        }
    }
}
