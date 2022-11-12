using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.SceneManagement;

namespace Bombaman.GameStates
{
    public abstract class GameStateBase
    {
        private int levelIndex = 0;

        private List<StateType> targetStates = new List<StateType>();

        public abstract string SceneName { get; }

        public abstract StateType Type { get; }

        public virtual bool IsAdditive { get { return false; } }

        public virtual int LevelIndex
        {
            get { return levelIndex; }
            protected set { levelIndex = value; }
        }

        protected GameStateBase() {}

        protected void AddTargetState(StateType targetState)
        {
            if (!targetStates.Contains(targetState))
            {
                targetStates.Add(targetState);
            }
        }

        protected void RemoveTargetState(StateType targetState)
        {
            targetStates.Remove(targetState);
        }

        public virtual void Activate(int levelIndex = 0, bool forceLoad = false)
        {
            Scene currentScene = SceneManager.GetActiveScene();
            if(forceLoad||currentScene.name.ToLower() != SceneName.ToLower())
            {
                LoadSceneMode mode = IsAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single;

                SceneManager.LoadScene(SceneName, mode);
            }
        }

        public virtual void Deactivate()
        {
            if (IsAdditive)
            {
                SceneManager.UnloadSceneAsync(SceneName);
            }
        }

        public bool IsValidTarget(StateType target)
        {
            foreach(StateType st in targetStates)
            {
                if(st == target)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
