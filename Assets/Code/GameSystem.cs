using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;


namespace Bombaman
{
    public class GameSystem : MonoBehaviour
    {
        private TextMeshProUGUI textWinner;
        private Character player;

        public bool Player1Alive = true;
        public bool Player2Alive = true;

        private GameObject[] enemies;

        [SerializeField] PauseMenu pauseMenu;
        // Start is called before the first frame update
        void Start()
        {
            player = FindObjectOfType<Character>();
            textWinner = FindObjectOfType<TextMeshProUGUI>();
            textWinner.enabled = false;
            
        }

        // Update is called once per frame
        void Update()
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");
            if (SceneManager.GetActiveScene().name.ToLower().StartsWith ("level") &&  enemies.Length == 0) // FOR SINGLEPLAYER WIN CHECKING.
			{
                textWinner.enabled = true;
                textWinner.text = ("Player won! Going to menu in 5secs.");
                Invoke("BackToMenu", 5f); //TODO: Load next singleplayer scene.
            }


            
            if (SceneManager.GetActiveScene().name.ToLower().StartsWith("level") && Player1Alive == false) // FOR SINGLEPLAYER WIN CHECKING.
            {
                textWinner.enabled = true;
                textWinner.text = ("Player died! Going to menu in 5secs.");
                Invoke("BackToMenu", 5f);
            }
            if (SceneManager.GetActiveScene().name == "2Player" && Player1Alive == false)
			{
                textWinner.enabled = true;
                textWinner.text = ("Player 2 is a winner! Going to menu in 5secs.");
                Invoke("BackToMenu", 5f);
			}
            if (SceneManager.GetActiveScene().name == "2Player" && Player2Alive == false)
            {
                textWinner.enabled = true;
                textWinner.text = ("Player 1 is a winner! Going to menu in 5secs.");
                Invoke("BackToMenu", 5f);
            }
        }


        

        public void BackToMenu()
		{      
            //pauseMenu.Paused = false;
            //DestroyAllGameObjects();
            GameStateManager.Instance.Go(GameStates.StateType.MainMenu);
		}

        public void DestroyAllGameObjects() // Destroys all gameobjects so can go back to mainmenu
        {
            GameObject[] GameObjects = (FindObjectsOfType<GameObject>() as GameObject[]);

            for (int i = 0; i < GameObjects.Length; i++)
            {
                Destroy(GameObjects[i]);
            }
        }
    }
}
