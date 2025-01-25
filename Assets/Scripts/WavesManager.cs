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
            new Wave(10, ElementType.Yellow, 2.5f, 100),
            new Wave(15, ElementType.Red, 3.0f, 80),
            new Wave(20, ElementType.Blue, 1.5f, 120),
            new Wave(12, ElementType.Green, 2.8f, 90),
            new Wave(18, ElementType.Red, 3.5f, 70),
            new Wave(3, ElementType.Green, 0.5f, 270),
            new Wave(3, ElementType.Blue, 1.5f, 110)
        };


    void Start()
    {
        cooldownTimer = 0;
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
                StartCoroutine(SpawnWave(wavesList[currentWave]));
                waveLevel++; // Move to the next wave
                cooldownTimer = waveCooldown; // Reset the cooldown timer
            }
    }

    IEnumerator SpawnWave(Wave wave) {
        foreach (GameObject spawner in GameManager.Instance.LevelsManager.spawnerList)
        {
            if (currentWave >= wavesList.Count) {currentWave = 0;}
            spawner.GetComponent<Spawner>().spawnBubble(wave);
            yield return new WaitForSeconds(1f);  
            currentWave++;
        }
    }
}
