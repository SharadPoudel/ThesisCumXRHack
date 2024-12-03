using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using Fusion;
using System.Linq;  // Import LINQ for .ToList() and .Count() methods

public class PlayerSpawner : MonoBehaviour
{
    // Assign spawn points in the Unity editor
    public Transform spawnPoint1;
    public Transform spawnPoint2;

    // Reference to the player prefab, must be assigned in the Unity Editor
    public GameObject playerPrefab;

    // Called when a player is spawned
    public void SpawnPlayer(NetworkRunner runner, PlayerRef playerRef)
    {
        // Get the current number of players in the session
        int playerCount = runner.ActivePlayers.ToList().Count;  // Convert ActivePlayers to List and get Count

        // Determine the spawn location and orientation based on player join order
        Vector3 spawnPosition;
        Quaternion spawnRotation;

        if (playerCount == 1)
        {
            // First player
            spawnPosition = spawnPoint1.position;
            spawnRotation = spawnPoint1.rotation;
        }
        else if (playerCount == 2)
        {
            // Second player
            spawnPosition = spawnPoint2.position;
            spawnRotation = spawnPoint2.rotation;
        }
        else
        {
            // Additional players can be assigned other spawn points or use random locations if needed
            spawnPosition = spawnPoint2.position; // Adjust for more players if needed
            spawnRotation = spawnPoint2.rotation;
        }

        // Instantiate the player prefab at the chosen spawn point
        runner.Spawn(
            playerPrefab,        // Reference to your player prefab
            spawnPosition,       // Chosen position
            spawnRotation,       // Chosen orientation
            playerRef            // Player reference to track ownership
        );
    }
}