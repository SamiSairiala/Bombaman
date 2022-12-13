using Bombaman.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Bombaman
{
    public class CreditsState : GameStateBase
    {
        public override string SceneName
        {
            get { return "Credits"; }
        }

        public override StateType Type
        {
            get { return StateType.Credits; }
        }

        public CreditsState() : base()
        {
            AddTargetState(StateType.MainMenu);
        }
    }
}
