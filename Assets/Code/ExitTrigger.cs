using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
			if(collision.tag == "Player")
			{
                // TODO: LOAD DIFFRENT SCENE.
			}
		}
	}
}
