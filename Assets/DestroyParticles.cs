using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class DestroyParticles : MonoBehaviour
    {
        // Start is called before the first frame update
        void Start()
        {
            Invoke("Delete", 1.5f);
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void Delete()
		{
            Destroy(gameObject);
		}
    }
}
