using UnityEngine;
using UnityEngine.InputSystem;

public class MultiplayerController : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private void Awake()
    {
        playerInputManager = GetComponent<PlayerInputManager>();
    }

    public void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex + 1} joined with device {playerInput.devices[0].name}");
    }

    public void OnPlayerLeft(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex + 1} left");
        Destroy(playerInput.gameObject);
    }
}