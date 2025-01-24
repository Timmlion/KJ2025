using UnityEngine;

public class WavesManager : MonoBehaviour
{
    public int waveLevel = 1;
    public int waveCooldown = 30;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
            if (Input.GetButtonUp("Fire1"))
            SpawnWave();
    }

    public void SpawnWave() {
        foreach (GameObject spawner in GameManager.Instance.LevelsManager.spawnerList)
        {
            spawner.GetComponent<Spawner>().spawnBubble();
        }
    }
}
