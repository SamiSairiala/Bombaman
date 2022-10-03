using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
       public void DestroyAfter(float seconds)
        {
            Destroy(gameObject, seconds);
        }

    }
}
