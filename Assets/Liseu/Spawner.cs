using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bubblePrefab;

    void Start()
    {
        
    }

    void spawnBubble() {
        Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
    }

}
