using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bubblePrefab;

    void Start()
    {
    }

    void Update()
    {
    }

    public void spawnBubble() {
        Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
        Debug.Log("reee");
    }

    public void spawnBubble(int amount, ElementType type) {
        for (int i = 0; i < amount; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
            bubble.GetComponent<Bubble>().vulnerability = type;
        }
    }

}
