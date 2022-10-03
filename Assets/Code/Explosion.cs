using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

namespace Bombaman
{
    public class Explosion : MonoBehaviour
    {
        private Bomb bombScript;

        private bool Chained = false;

        //private void Start()
        //{
        //    Invoke("Destroy", 2f);
        //}

        public void OnTriggerEnter2D(Collider2D other)
        { // Checks if the bomb has exploded // Check if what the explosion touches has tag "Explosion"
            if (other.tag == "Bomb" && Chained == false)
            {
                Debug.Log("Found a bomb");
                bombScript = other.gameObject.GetComponent<Bomb>();
                if (bombScript.exploded == false)
                {
                    Chained = true;

                    Debug.Log("Chain reaction!");
                    bombScript.cancelInvoke();

                    bombScript.Explode();
                }

            }
            if(other.tag == "Breakable")
            {
                Debug.Log("Breaking a wall");
                Tilemap tilemap = other.transform.gameObject.GetComponent<Tilemap>();
                //tilemap.SetTile(other.transform.position.x, null);
            }
        }

        private void OnDestroy()
        {
            Destroy(gameObject);
        }
    }
}
