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
            bubble.GetComponent<Bubble>().health = wave.health + 5 * waveLevel;
            bubble.GetComponent<Bubble>().speed = wave.speed + .05f * waveLevel;
            bubble.transform.localScale = baseScale * CalculateScale(bubble.GetComponent<Bubble>().health = wave.health + 5 * GameManager.Instance.WavesManager.waveLevel);
        }
    }

    private float CalculateScale(float health)
    {
        // Define scaling rules
        float scaleAt30HP = .9f; 
        float scaleAt90HP = 1.3f;   
        float scaleAt270HP = 2.2f;  

        // Interpolate between the scale values
        if (health <= 90f)
        {
            // Scale from 30 HP (0.5x) to 90 HP (1x)
            return Mathf.Lerp(scaleAt30HP, scaleAt90HP, (health - 30f) / (90f - 30f));
        }
        else
        {
            // Scale from 90 HP (1x) to 270 HP (2x)
            return Mathf.Lerp(scaleAt90HP, scaleAt270HP, (health - 90f) / (270f - 90f));
        }
    }

}
