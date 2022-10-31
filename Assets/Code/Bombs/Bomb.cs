using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
//using System.Diagnostics;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UIElements;

namespace Bombaman
{
    public class Bomb : MonoBehaviour
    {
        private bool hit = false;
        private BombController controller;

        [SerializeField] private float kickForce = 1f; // How hard players can kick it. 

        private Rigidbody2D rigidbody;
        private void Awake()
        {
            hit = false;
            controller = FindObjectOfType<BombController>();
            rigidbody = GetComponent<Rigidbody2D>();
        }
        #region old bomb script.
        //[SerializeField] private GameObject explosionPrefab;
        //public LayerMask levelMask;
        //public bool exploded = false;

        //[SerializeField] private Tilemap Desctructible;

        //// TODO: When we have bombs change collider type
        //[SerializeField]private CapsuleCollider2D collider;



        //// Start is called before the first frame update
        //private void Start()
        //{

        //    Invoke("Explode", 3f);
        //    Invoke("EnableCollider", 0.4f);
        //    Debug.Log(LayerMask.NameToLayer("Walls"));
        //    Debug.Log(LayerMask.NameToLayer("Blocks"));


        //}

        //private void Awake()
        //{
        //    levelMask = LayerMask.GetMask("Default", "Walls", "Blocks");
        //}

        //private void EnableCollider()
        //{
        //    collider.enabled = true;
        //    Debug.Log("Enabling collider");

        //}

        //public void Explode()
        //{



        //        Instantiate(explosionPrefab, transform.position, Quaternion.identity); // Spawns an explosion at bomb's location
        //        collider.enabled = false;
        //        StartCoroutine(CreateExplosions(Vector2.up)); // The StartCoroutine calls will start up the CreateExplosions IEnumerator once for every direction.
        //        StartCoroutine(CreateExplosions(Vector2.right));
        //        StartCoroutine(CreateExplosions(Vector2.down));
        //        StartCoroutine(CreateExplosions(Vector2.left));

        //        GetComponent<SpriteRenderer>().enabled = false; // Disables mesh renderer making the bomb invisible.

        //        Debug.Log("Exploding");
        //        //transform.Find("Collider").gameObject.SetActive(false);
        //        exploded = true;
        //        Destroy(gameObject, .3f); // Destroys the bomb after 0.3 seconds; this ensures all explosions will spawn before the GameObject is destroyed.

        //}

        //public void cancelInvoke()
        //{
        //    CancelInvoke("Explode");
        //}


        //public void OnTriggerEnter2D(Collider2D other)
        //{ // Checks if the bomb has exploded // Check if what the explosion touches has tag "Explosion"
        //    if (other.CompareTag("Explosion"))
        //    {
        //        if(exploded == false)
        //        {
        //            Debug.Log("Chain reaction!");
        //            CancelInvoke("Explode");

        //            Explode();
        //        }

        //    }
        //}
        //private IEnumerator CreateExplosions(Vector3 direction)
        //{
        //    //Iterates a for loop for every unit of distance you want the explosions to cover. In this case, the explosion will reach two meters.
        //    for (int i = 1; i < 3; i++)
        //    {

        //        //sends out a raycast from the center of the bomb towards
        //        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction,
        //          i, levelMask);
        //        //A RaycastHit object holds all the information about what and at which position the Raycast hits -- or doesn't hit.

        //        // If the raycast doesn't hit anything then it's a free space.
        //        if (hit.transform.gameObject.layer != 6)
        //        {

        //            //Spawns an explosion at the position the raycast checked.
        //            Instantiate(explosionPrefab, transform.position + (i * direction),
        //              explosionPrefab.transform.rotation);

        //        }
        //        if (hit.transform.gameObject.layer == 7)
        //        {

        //            Tilemap tilemap = hit.collider.GetComponent<Tilemap>();
        //            Vector3Int tilePos = tilemap.WorldToCell(hit.point);
        //            tilemap.SetTile(tilePos, null);
        //            Debug.Log($"Position of tile removed: x:{tilePos.x} y:{tilePos.y} z:{tilePos.z}");
        //            Instantiate(explosionPrefab, transform.position + (i * direction),
        //              explosionPrefab.transform.rotation);

        //        }
        //        if(hit.transform.gameObject.layer == 6)
        //        { //Once the raycast hits a block, it breaks out of the for loop. This ensures the explosion can't jump over walls.

        //            break;


        //        }

        //        //Waits for 0.05 seconds before doing the next iteration of the for loop. This makes the explosion more convincing by making it look like it's expanding outwards.
        //        yield return new WaitForSeconds(.05f);
        //    }
        //}
        #endregion

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if(collision.gameObject.layer == LayerMask.NameToLayer("Explosion") && hit == false)
            {
                hit = true;

                controller.StartExplosion(transform.position, gameObject);
                

                
            }
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if(collision.transform.name == "Player" && collision.transform.GetComponent<Character>().kicking) // Checks if player is kicking
            {
                
                Vector3 direction = (collision.transform.position - transform.position).normalized;
                rigidbody.AddForce(-direction * kickForce, ForceMode2D.Impulse); // To make it move less edit kickForce.
            }
        }




    }
}