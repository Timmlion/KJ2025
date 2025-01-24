using UnityEngine;
using UnityEngine.InputSystem;

public class PlayersManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;
    
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined the game.");

        // Optionally, assign a unique spawn point or set up the player
        Vector3 spawnPosition = new Vector3(playerInput.playerIndex * 2f, 0, 0);
        playerInput.gameObject.transform.position = spawnPosition;
        playerInput.gameObject.GetComponent<PlayerController>().InitializePlayer(playerInput);
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} left the game.");
        Destroy(playerInput.gameObject);
    }
}
