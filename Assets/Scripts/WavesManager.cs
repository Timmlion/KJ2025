using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WavesManager : MonoBehaviour
{
    public int waveLevel = 0;
    public int waveCooldown = 10;
    public int currentWave = 0;
    float cooldownTimer;

    public List<GameObject> spawnerList;
    

    public List<Wave> wavesList =  new List<Wave>
        {
            new Wave(4, ElementType.Yellow, 0.5f, 80),
            new Wave(5, ElementType.Red, 1.0f, 50),
            new Wave(7, ElementType.Blue, 1.2f, 40),
            new Wave(5, ElementType.Green, 0.9f, 90),
            new Wave(10, ElementType.Red, 0.5f, 60),
            new Wave(2, ElementType.Green, 0.2f, 270),
            new Wave(3, ElementType.Blue, 1.0f, 110),
            new Wave(5, ElementType.Yellow, 1.2f, 60)
        };


    void Start()
    {
        cooldownTimer = 1;
        spawnerList = GameManager.Instance.LevelsManager.spawnerList;
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
                cooldownTimer = waveCooldown; // Reset the cooldown timer
            }
            
            if (Input.GetKeyDown(KeyCode.Alpha6)) 
            {
                PlayerPrefs.SetInt("lastWave", 0);
            }

            if (Input.GetKeyDown(KeyCode.Comma)) {StartCoroutine(SpawnWave(wavesList));}
    }

    IEnumerator SpawnWave(List<Wave> waveList) {
        waveLevel++; // Move to the next wave
        GameManager.Instance.LevelsManager.ShowWaveLabel(waveLevel);
        PlayerPrefs.SetInt("lastWave", waveLevel);
        
        var topWave = PlayerPrefs.GetInt("topWave", waveLevel);
        if (waveLevel > topWave)
        {
            PlayerPrefs.SetInt("topWave", waveLevel);
        }
        
        if (waveLevel % 2 == 0)
        {
            GameObject spawner = spawnerList[Random.Range(0, spawnerList.Count)];
            Spawner randomSpawner = spawner.GetComponent<Spawner>();
            randomSpawner.SpawnChungus(waveList[currentWave], waveLevel);
        }
        foreach (GameObject spawner in GameManager.Instance.LevelsManager.spawnerList)
        {
            if (currentWave >= wavesList.Count) {currentWave = 0;}
            spawner.GetComponent<Spawner>().spawnEnemies(waveList[currentWave], waveLevel);
            yield return new WaitForSeconds(1f);  
            currentWave++;
        }
    }

}
