using Bombaman.GameStates;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bombaman
{
    public class GameStateManager : MonoBehaviour
    {
        private static GameStateManager instance;

        public static GameStateManager Instance
        {
            get
            {
                if(instance == null)
                {
                    GameStateManager prefab = Resources.Load<GameStateManager>(typeof(GameStateManager).Name);

                    instance = Instantiate(prefab);
                }
                return instance;
            }
        }

        private List<GameStateBase> states = new List<GameStateBase>();

        public GameStateBase CurrentState { get; private set; }
        public GameStateBase PreviousState { get; private set; }

        private void Awake()
        {
            if (instance == null) { instance = this; }
            else if (instance != this) { Destroy(this); return; }

            DontDestroyOnLoad(gameObject);

            Initialize();
        }

        private void Initialize()
        {
            MainMenuState mainMenu = new MainMenuState();
            OptionsState options = new OptionsState();
            InGameState inGame = new InGameState();
            LobbyState lobby = new LobbyState();
            MultiplayerState multiplayer = new MultiplayerState();
            GameOverState gameOver = new GameOverState();

            states.Add(mainMenu);
            states.Add(options);
            states.Add(inGame);
            states.Add(lobby);
            states.Add(multiplayer);
            states.Add(gameOver);

            foreach(GameStateBase state in states)
            {
                string activeSceneName = SceneManager.GetActiveScene().name.ToLower();
                int index = 0;
                if (activeSceneName.StartsWith("level"))
                {
                    index = int.Parse(activeSceneName.Substring(5, 1));
                    activeSceneName = "level";
                }

                string sceneName = state.SceneName.ToLower();
                if (sceneName.StartsWith("level"))
                {
                    sceneName = "level";
                }

                if(sceneName == activeSceneName)
                {
                    ActivateFirstScene(state, index);
                    break;
                }
            }

            if(CurrentState == null)
            {
                ActivateFirstScene(mainMenu);
            }

        }

        private void ActivateFirstScene(GameStateBase first, int index = 0)
        {
            CurrentState = first;
            CurrentState.Activate(index);
        }

        private GameStateBase GetState(StateType type)
        {
            foreach(GameStateBase state in states)
            {
                if(state.Type == type)
                {
                    return state;
                }
            }

            return null;
        }

        public bool Go(StateType targetStateType, int levelIndex = 0)
        {
            if (!CurrentState.IsValidTarget(targetStateType)) { return false; }

            GameStateBase nextState = GetState(targetStateType);
            if(nextState == null) { return false; }

            PreviousState = CurrentState;
            CurrentState.Deactivate();
            CurrentState = nextState;
            CurrentState.Activate(levelIndex);

            return true;
        }

        public bool GoBack()
        {
            return Go(PreviousState.Type, PreviousState.LevelIndex);
        }
    }
}
