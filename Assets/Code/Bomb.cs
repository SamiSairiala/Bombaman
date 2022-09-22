using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class Bomb : MonoBehaviour
    {
        [SerializeField] private GameObject explosionPrefab;
        public LayerMask levelMask;

        // Start is called before the first frame update
        private void Start()
        {
            Invoke("Explode", 3f);
        }

        private void Explode()
        {
            Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Spawns an explosion at bomb's location
            StartCoroutine(CreateExplosions(Vector2.up)); // The StartCoroutine calls will start up the CreateExplosions IEnumerator once for every direction.
            StartCoroutine(CreateExplosions(Vector2.right));
            StartCoroutine(CreateExplosions(Vector2.down));
            StartCoroutine(CreateExplosions(Vector2.left));
            GetComponent<SpriteRenderer>().enabled = false; // Disables mesh renderer making the bomb invisible.
            Destroy(gameObject, .3f); // Destroys the bomb after 0.3 seconds; this ensures all explosions will spawn before the GameObject is destroyed.
        }

        private IEnumerator CreateExplosions(Vector3 direction)
        {
            //Iterates a for loop for every unit of distance you want the explosions to cover. In this case, the explosion will reach two meters.
            for (int i = 1; i < 3; i++)
            {

                //sends out a raycast from the center of the bomb towards
                RaycastHit2D hit = Physics2D.Raycast(transform.position + new Vector3(0, .5f, 0), direction,
                  i, levelMask);
                //A RaycastHit object holds all the information about what and at which position the Raycast hits -- or doesn't hit.
               
                //If the raycast doesn't hit anything then it's a free tile.
                if (!hit.collider)
                {
                    Instantiate(explosionPrefab, transform.position + (i * direction),
                      //Spawns an explosion at the position the raycast checked.
                      explosionPrefab.transform.rotation);
                    //The raycast hits a block.
                }
                else
                { //Once the raycast hits a block, it breaks out of the for loop. This ensures the explosion can't jump over walls.
                    break;
                    
                }

                //Waits for 0.05 seconds before doing the next iteration of the for loop. This makes the explosion more convincing by making it look like it's expanding outwards.
                yield return new WaitForSeconds(.05f);
            }
        }

        
    }
}