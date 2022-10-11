using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class PlayerSpawner : MonoBehaviour
    {

        public GameObject[] spawnLocations; // Spawn points


        void OnPlayerJoined(PlayerInput playerInput)
        {

            Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
            // Set the player ID, add one to the index to start at Player 1
            playerInput.gameObject.GetComponent<Character>().playerID = playerInput.playerIndex + 1;

            // Set the start spawn position of the player using the location at the associated element into the array.
            playerInput.gameObject.GetComponent<Character>().startPosition = spawnLocations[playerInput.playerIndex].transform.position;

        }
    }
}
