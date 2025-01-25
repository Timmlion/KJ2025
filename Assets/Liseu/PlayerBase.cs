using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int startingHealth = 100;
    public int health = 100;
    public float currentHealth = 100;
    public HPBar hpBar;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        currentHealth = health;
        hpBar.UpdateHealthBar(currentHealth/startingHealth);
        if (health <= 0) { DestroyBase();}
    }

    public void DestroyBase()
    {
        Camera.main.GetComponent<CameraShake>().TriggerShake();
        GameManager.Instance.LevelsManager.playerBaseList.Remove(this.gameObject);
        Destroy(gameObject);
    }
}
