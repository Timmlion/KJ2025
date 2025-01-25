using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : MonoBehaviour
{
    public int waveLevel = 0;
    public int waveCooldown = 10;
    public int currentWave = 0;
    float cooldownTimer;

    public List<Wave> wavesList =  new List<Wave>
        {
            new Wave(7, ElementType.Yellow, 0.5f, 90),
            new Wave(5, ElementType.Red, 1.0f, 60),
            new Wave(10, ElementType.Blue, 1.5f, 150),
            new Wave(5, ElementType.Green, 0.9f, 90),
            new Wave(10, ElementType.Red, 0.5f, 70),
            new Wave(3, ElementType.Green, 0.2f, 270),
            new Wave(5, ElementType.Blue, 1.0f, 110)
        };


    void Start()
    {
        cooldownTimer = 1;
    }

    // Update is called once per frame
    void Update()
    {
            // Decrement the cooldown timer
            cooldownTimer -= Time.deltaTime;

            // If the cooldown timer reaches 0, spawn the next wave
            if (cooldownTimer <= 0f)
            {
                if (currentWave >= wavesList.Count) {currentWave = 0;}
                StartCoroutine(SpawnWave(wavesList));
                waveLevel++; // Move to the next wave
                cooldownTimer = waveCooldown; // Reset the cooldown timer
            }
    }

    IEnumerator SpawnWave(List<Wave> waveList) {
        foreach (GameObject spawner in GameManager.Instance.LevelsManager.spawnerList)
        {
            if (currentWave >= wavesList.Count) {currentWave = 0;}
            spawner.GetComponent<Spawner>().spawnBubble(waveList[currentWave]);
            yield return new WaitForSeconds(1f);  
            currentWave++;
        }
    }
}
