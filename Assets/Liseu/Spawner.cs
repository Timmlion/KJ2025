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

    public void spawnBubble(Wave wave) {
        for (int i = 0; i < wave.amount; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
            bubble.GetComponent<Bubble>().vulnerability = wave.type;
            bubble.GetComponent<Bubble>().health = wave.health + 5 * GameManager.Instance.WavesManager.waveLevel;
            bubble.GetComponent<Bubble>().speed = wave.speed + .05f * GameManager.Instance.WavesManager.waveLevel;
        }
    }

}
