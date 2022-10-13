using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Bombaman
{
    public class PlayerManager : MonoBehaviour
    {

        public GameObject[] spawnLocations; // Spawn points

        private int PlayerCount = 0;
        
        void OnPlayerJoined(PlayerInput playerInput)
        {

            Debug.Log("PlayerInput ID: " + playerInput.playerIndex);
            // Set the player ID, add one to the index to start at Player 1
            playerInput.gameObject.GetComponent<Character>().playerID = playerInput.playerIndex + 1; // Player 1 = 0, Player 2 = 1 etc.

            // Set the start spawn position of the player using the location at the associated element into the array.
            playerInput.gameObject.GetComponent<Character>().startPosition = spawnLocations[playerInput.playerIndex].transform.position;

            
            PlayerCount++;
        }

        private void Update()
        {
            // TODO: Add scenemanagement to see which scene we want for example 2 player or 4 player. In singleplayer this gameobject isn't even activated so no need to add that
            if(PlayerCount < 2) // Here to "pause" the game to wait for 2nd player.
            {
                Time.timeScale = 0; // TODO: Add UI to message the players to press any button to join
            }
            if(PlayerCount == 2) // Resumes game when both players are joined.
            {
                Time.timeScale = 1;
            }
            
        }
    }
}
