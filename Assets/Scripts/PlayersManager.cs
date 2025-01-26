using System;
using System.Collections.Generic;
using System.Linq;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Serialization;
using Random = UnityEngine.Random;

public class PlayersManager : MonoBehaviour
{
    public bool AllowSameColorPlayers = true;

    private PlayerInputManager playerInputManager;

    [SerializeField] private List<PlayerController> players = new();

    private void OnEnable()
    {
        InputSystem.onDeviceChange += OnDeviceChange;
    }

    private void OnDisable()
    {
        InputSystem.onDeviceChange -= OnDeviceChange;
    }

    private void OnDeviceChange(InputDevice device, InputDeviceChange change)
    {
        if (device is Gamepad gamepad)
        {
            switch (change)
            {
                case InputDeviceChange.Disconnected:
                    HandleControllerDisconnect(gamepad);
                    break;
            }
        }
    }

    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined the game.");

        var playerController = playerInput.gameObject.GetComponent<PlayerController>();
        players.Add(playerController);

        var tower = GameManager.Instance.TowersManager.GetRandomFreeTower();

        playerController.SetCurrentTower(tower);
        playerController.transform.position = tower.transform.position;
        playerController.SetElementType(GetRandomUnassignedElementType());

        playerController.GetComponent<PlayerController>().InitializePlayer(playerInput);
    }

    private void OnPlayerLeft(PlayerInput playerInput)
    {
        var playerController = playerInput.gameObject.GetComponent<PlayerController>();
        players.Remove(playerController);
        Debug.Log($"Player {playerInput.playerIndex} left the game.");
        Destroy(playerInput.gameObject);
    }

    private void HandleControllerDisconnect(Gamepad gamepad)
    {
        var playerController = players.Find(
            p =>
                p.PlayerInput == null ||
                p.PlayerInput.devices.Count == 0 ||
                p.PlayerInput.devices.Contains(gamepad)
        );
        if (playerController != null)
        {
            players.Remove(playerController);
            playerController.Remove();
        }
    }

    public bool IsElementTypeFree(ElementType elementType)
    {
        if (AllowSameColorPlayers) return true;
        return !players.Any(p => p.CurrentElementType == elementType);
    }

    public ElementType GetRandomUnassignedElementType()
    {
        var allElementTypes = Enum.GetValues(typeof(ElementType)).Cast<ElementType>();

        var assignedElementTypes = Enumerable.ToHashSet(players.Select(p => p.CurrentElementType));

        var availableElementTypes = allElementTypes.Where(et => !assignedElementTypes.Contains(et)).ToList();

        if (availableElementTypes.Count > 0)
        {
            var randomIndex = Random.Range(0, availableElementTypes.Count);
            return availableElementTypes[randomIndex];
        }

        return ElementType.Blue;
    }
}