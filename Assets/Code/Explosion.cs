using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
        private void Start()
        {
            Invoke("Destroy", 2f);
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}
