using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject bubblePrefab;
    public Vector3 baseScale = new Vector3(1f, 1f, 1f);

    public void spawnEnemies(Wave wave, int waveLevel) {
        int waveGrowth = wave.amount + (int)waveLevel/3;
        for (int i = 0; i < wave.amount; i++)
        {
            Bubble bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity).GetComponent<Bubble>();
            bubble.SetVulnerability(wave.type);
            bubble.health = wave.health + 20 * waveLevel;
            bubble.startingHealth = bubble.health;
            bubble.navMeshAgent.speed = wave.speed + .4f * waveLevel;
            bubble.transform.localScale = baseScale * CalculateScale(bubble.health);
        }
    }

    public void SpawnChungus(Wave wave, int waveLevel) {
        Debug.Log("Spawning chungus");
        Bubble bubble = Instantiate(bubblePrefab, this.transform.position, Quaternion.identity).GetComponent<Bubble>();
        bubble.SetVulnerability(wave.type);
        bubble.health = wave.health * 10 + 20 * waveLevel;
        bubble.startingHealth = bubble.health;
        bubble.navMeshAgent.speed  += .4f * waveLevel;
        bubble.name = "BigChungus";
        bubble.damage = 1000;
        bubble.transform.localScale = baseScale * CalculateScale(bubble.health);
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
