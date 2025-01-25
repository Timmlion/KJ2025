using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.InputSystem;
using Random = UnityEngine.Random;

public class PlayersManager : MonoBehaviour
{
    private PlayerInputManager playerInputManager;

    private List<PlayerController> players = new ();
    
    private void OnPlayerJoined(PlayerInput playerInput)
    {
        Debug.Log($"Player {playerInput.playerIndex} joined the game.");

        // // Optionally, assign a unique spawn point or set up the player
        // Vector3 spawnPosition = new Vector3(playerInput.playerIndex * 2f, 0, 0);
        // playerInput.gameObject.transform.position = spawnPosition;
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

    public bool IsElementTypeFree(ElementType elementType)
    {
        return players.Any(p => p.ElementType == elementType);
    }
    
    public ElementType GetRandomUnassignedElementType()
    {
        var allElementTypes = Enum.GetValues(typeof(ElementType)).Cast<ElementType>();
        
        var assignedElementTypes = players.Select(p => p.ElementType).ToHashSet();
        
        var availableElementTypes = allElementTypes.Where(et => !assignedElementTypes.Contains(et)).ToList();

        if (availableElementTypes.Count == 0)
        {
            Debug.LogError("All elements are assigned!");
        }
        
        var randomIndex = Random.Range(0, availableElementTypes.Count);
        return availableElementTypes[randomIndex];
    }
}
