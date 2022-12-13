using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class MainMenuState : GameStateBase
    {
        public override string SceneName
        {
            get { return "MainMenu"; }
        }

        public override StateType Type
        {
            get { return StateType.MainMenu; }
        }

        public MainMenuState() : base()
        {
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.Lobby);
            AddTargetState(StateType.Options);
            AddTargetState(StateType.Credits);
        }
    }
}
