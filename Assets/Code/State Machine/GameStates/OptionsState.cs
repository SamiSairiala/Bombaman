using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class OptionsState : GameStateBase
    {
        public override string SceneName
        {
            get { return "Options";  }
        }

        public override StateType Type
        {
            get { return StateType.Options; }
        }

        public override bool IsAdditive
        {
            get { return true; }
        }

        public OptionsState() : base()
        {
            AddTargetState(StateType.InGame);
            AddTargetState(StateType.MainMenu);
            AddTargetState(StateType.Multiplayer);
        }
    }
}
