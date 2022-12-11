using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bombaman
{
    public class ExitTrigger : MonoBehaviour
    {
        private SpriteRenderer spriteRenderer;

        private Collider2D trigger;

        [SerializeField]private Sprite SpriteExitActivated;
        // Start is called before the first frame update
        void Start()
        {
            trigger = GetComponent<Collider2D>();
            spriteRenderer = GetComponent<SpriteRenderer>();
        }

        public void ChangeSprite()
		{
            spriteRenderer.sprite = SpriteExitActivated;
		}

		private void OnTriggerEnter2D(Collider2D collision)
		{
			if(collision.tag == "Player" && SceneManager.GetActiveScene().name == "level0") // If making more levels add here
			{
                SceneManager.LoadScene("level1");// TODO: LOAD DIFFRENT SCENE.
			}
            if (collision.tag == "Player" && SceneManager.GetActiveScene().name == "level1")
            {
                Debug.Log("Finished level 2");
                FindObjectOfType<GameSystem>().Player1SingleWinner = true;
                SceneManager.LoadScene("GameOver");// TODO: LOAD DIFFRENT SCENE.
            }
        }
	}
}
