using Bombaman.GameStates;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;

namespace Bombaman
{
    public class InGameState : GameStateBase
    {
        public override string SceneName
        {
            get { return "level" + LevelIndex; }
        }

        public override StateType Type
        {
            get { return StateType.InGame; }
        }

        public InGameState() : base()
        {
            AddTargetState(StateType.Options);
            AddTargetState(StateType.GameOver);
        }

        public override void Activate(int levelIndex = 0, bool forceLoad = false)
        {
            LevelIndex = levelIndex;

            base.Activate(levelIndex, forceLoad);

            Time.timeScale = 1;
        }

        public override void Deactivate()
        {
            base.Deactivate();

            Time.timeScale = 0;
        }
    }
}
