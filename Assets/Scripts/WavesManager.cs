using UnityEngine;

public class WavesManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
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
