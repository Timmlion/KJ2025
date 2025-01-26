using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Vector3 baseScale = new Vector3(1f, 1f, 1f);

    public void spawnEnemies(Wave wave, int waveLevel) {
        int waveGrowth = wave.amount + (int)waveLevel/3;
        for (int i = 0; i < wave.amount; i++)
        {
            GameObject bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
            bubble.GetComponent<Bubble>().SetVulnerability(wave.type);
            bubble.GetComponent<Bubble>().health = wave.health + 10 * waveLevel;
            bubble.GetComponent<Bubble>().navMeshAgent.speed = wave.speed + 2f * waveLevel;
            bubble.transform.localScale = baseScale * CalculateScale(bubble.GetComponent<Bubble>().health = wave.health + 5 * GameManager.Instance.WavesManager.waveLevel);
        }
    }

    public void SpawnChungus(Wave wave, int waveLevel) {
        Debug.Log("Spawning chungus");
        GameObject bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity);
            bubble.GetComponent<Bubble>().SetVulnerability(wave.type);
            bubble.GetComponent<Bubble>().health = wave.health *10 + 10 * waveLevel;
            bubble.GetComponent<Bubble>().navMeshAgent.speed  += 2f * waveLevel;
            bubble.name = "BigChungus";
            bubble.transform.localScale = baseScale * CalculateScale(bubble.GetComponent<Bubble>().health = wave.health + 5 * GameManager.Instance.WavesManager.waveLevel);
    }
        

    private float CalculateScale(float health)
    {
        float scaleAt30HP = 0.9f;
        float scaleAt90HP = 1.3f;
        float scaleAt270HP = 2.2f;
        float scaleAt1200HP = 10f;

        // Handle health ranges
        if (health <= 30f)
        {
            return scaleAt30HP;
        }
        else if (health <= 90f)
        {
            return Mathf.Lerp(scaleAt30HP, scaleAt90HP, (health - 30f) / (90f - 30f));
        }
        else if (health <= 270f)
        {
            return Mathf.Lerp(scaleAt90HP, scaleAt270HP, (health - 90f) / (270f - 90f));
        }
        else if (health <= 1200f)
        {
            return Mathf.Lerp(scaleAt270HP, scaleAt1200HP, (health - 270f) / (1200f - 270f));
        }
        else
        {
            return scaleAt1200HP;
        }
    }

}
