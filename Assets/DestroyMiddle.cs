using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class DestroyMiddle : MonoBehaviour
    {

        private BombController bombController;

        // Start is called before the first frame update
        void Start()
        {
            bombController = FindObjectOfType<BombController>(); 
            Invoke("DeleteMiddle", bombController.explosionDuration);
        }

        // Update is called once per frame
        void Update()
        {
        
        }

        void DeleteMiddle()
		{
            Destroy(gameObject);
		}
    }
}
