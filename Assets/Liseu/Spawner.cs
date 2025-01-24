using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bubblePrefab;

    void Start()
    {
        spawnBubble();
    }

    void Update()
    {

    }

    public void spawnBubble() {
        Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
        Debug.Log("reee");
    }

}
