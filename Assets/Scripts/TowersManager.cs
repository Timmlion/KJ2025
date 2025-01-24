using System.Collections.Generic;
using UnityEngine;

public class TowersManager : MonoBehaviour
{
    public List<TowerController> towerControllerList;
    public TowerController GetRandomFreeTower()
    {
        // Filter the list to only include towers where Posessed is false
        List<TowerController> freeTowers = towerControllerList.FindAll(tower => !tower.Posessed);

        // Return a random tower from the freeTowers list if it has elements
        if (freeTowers.Count > 0)
        {
            int randomIndex = Random.Range(0, freeTowers.Count);
            return freeTowers[randomIndex];
        }

        // If no free towers are available, return null
        return null;
    }

    public TowerController JumpTower(bool right, TowerController currentTower)
    {
        if (currentTower == null || towerControllerList.Count == 0)
        {
            return null; // Return null if the current tower is null or the list is empty
        }
        currentTower.Posessed = false;

        // Get the index of the current tower
        int currentIndex = towerControllerList.IndexOf(currentTower);

        if (currentIndex == -1)
        {
            return null; // If the current tower is not in the list, return null
        }
        
        int nextIndex = right ? (currentIndex + 1) % towerControllerList.Count
            : (currentIndex - 1 + towerControllerList.Count) % towerControllerList.Count;
        
        // Loop to find the next free tower
        for (int i = 0; i < towerControllerList.Count; i++)
        {
            TowerController nextTower = towerControllerList[nextIndex];

            if (!nextTower.Posessed)
            {
                nextTower.Posessed = true; // Set the next tower's Posessed to true
                return nextTower;
            }

            // Move to the next index
            nextIndex = right ? (nextIndex + 1) % towerControllerList.Count
                : (nextIndex - 1 + towerControllerList.Count) % towerControllerList.Count;
        }
        // If no free tower is found, return null
        return null;
    }
    
    
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
