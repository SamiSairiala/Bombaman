using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Bombaman
{
    public class SpawnPowerups : MonoBehaviour
    {

        [SerializeField] private GameObject powerup;
        private int randomRange;


        private void Awake()
        {
            int randomRange = Random.Range(1, 101);
        }
        public void SpawnPowerup()
        {
            
        }
    }
}
