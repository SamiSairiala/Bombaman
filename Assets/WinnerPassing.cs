using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Bombaman
{
    public class WinnerPassing : MonoBehaviour
    {
        private GameSystem gameSystem;
        // Start is called before the first frame update

        private bool Player1IsWinner = false;
        private bool Player2IsWinner = false;
        private bool Player3IsWinner = false;
        private bool Player4IsWinner = false;

        private GameObject textHolder;
        private TextMeshProUGUI content;
        
        void Start()
        {
            gameSystem = FindObjectOfType<GameSystem>();
        }

        // Update is called once per frame
        void Update()
        {
            
            if(SceneManager.GetActiveScene().name == "2Player" || SceneManager.GetActiveScene().name == "3Player")
			{
                DontDestroyOnLoad(this.gameObject);
                if(gameSystem.Player1Winner == true)
				{
                    Player1IsWinner = true;
				}
                if (gameSystem.Player2Winner == true)
                {
                    Player2IsWinner = true;
                }
                if (gameSystem.Player3Winner == true)
                {
                    Player3IsWinner = true;
                }
                if (gameSystem.Player4Winner == true)
                {
                    Player4IsWinner = true;
                }
            }
            if(SceneManager.GetActiveScene().name == "GameOver")
			{
                textHolder = GameObject.Find("Content");
                content = textHolder.GetComponent<TextMeshProUGUI>();
                if(Player1IsWinner == true)
				{
                    content.text = ("Player 1 Won!");
                }
                if (Player2IsWinner == true)
                {
                    content.text = ("Player 2 Won!");
                }
                if (Player3IsWinner == true)
                {
                    content.text = ("Player 3 Won!");
                }
                if (Player4IsWinner == true)
                {
                    content.text = ("Player 4 Won!");
                }
                
			}
        }
    }
}
