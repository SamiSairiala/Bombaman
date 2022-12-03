using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class ExitTrigger : MonoBehaviour
    {

        private Collider2D trigger;
        // Start is called before the first frame update
        void Start()
        {
            trigger = GetComponent<Collider2D>();
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
