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
            if(SceneManager.GetActiveScene().name == "2Player" && Player1Alive == false)
			{
                textWinner.enabled = true;
                textWinner.text = ("Player 2 is a winner");
			}
            if (SceneManager.GetActiveScene().name == "2Player" && Player2Alive == false)
            {
                textWinner.enabled = true;
                textWinner.text = ("Player 1 is a winner");
            }
        }

        private void Winner()
		{
            if (SceneManager.GetActiveScene().name == "2Player")
            {
                
                textWinner.text = ("Player 2 is a winner");
            }
        }
    }
}
