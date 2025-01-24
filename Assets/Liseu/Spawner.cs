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
            if (Input.GetButtonUp("Fire1"))
            spawnBubble();
    }

    void spawnBubble() {
        Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
        Debug.Log("reee");
    }

}
