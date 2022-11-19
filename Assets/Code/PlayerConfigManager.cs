using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class PlayerConfigManager : MonoBehaviour
    {
        private List<PlayerConfig> playerConfigs;

        [SerializeField] private int MaxPlayers = 2;

        public static PlayerConfigManager instance { get; private set; }

		private void Awake()
		{
			if(instance != null)
			{
				
			}
			else
			{
				instance = this;
				DontDestroyOnLoad(instance);
				playerConfigs = new List<PlayerConfig>();
			}
		}

		public void ReadyPlayer(int index)
		{
			playerConfigs[index].isReady = true;
			if(playerConfigs.Count == MaxPlayers && playerConfigs.All(p => p.isReady == true))
			{
				SceneManager.LoadScene("LocalTest");
			}
			
		}

		public void HandlePlayerJoin(PlayerInput pi)
		{
			
			if(!playerConfigs.Any(p => p.PlayerIndex == pi.playerIndex))
			{
				pi.transform.SetParent(transform);
				playerConfigs.Add(new PlayerConfig(pi));
			}
		}

	}

    public class PlayerConfig
	{
		public PlayerConfig(PlayerInput PI)
		{
			PlayerIndex = PI.playerIndex;
			Input = PI;
		}
        public PlayerInput Input { get; set; }

        public int PlayerIndex { get; set; }

        public bool isReady { get; set; }
	}


}



