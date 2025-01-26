using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public int startingHealth = 100;
    public int health = 100;
    public float currentHealth = 100;
    public HPBar hpBar;
    private PlayerBaseAnimator playerBaseAnimator;

    private void Awake()
    {
        playerBaseAnimator = GetComponent<PlayerBaseAnimator>();
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        currentHealth = health;
        hpBar.UpdateHealthBar(currentHealth/startingHealth);
        if (health <= 0)
        {
            DestroyBase();
        }
        else
        {
            playerBaseAnimator.PlayTakeDamageAnimation();
        }
    }
    
    [ContextMenu("Debug - DestroyBase")]
    public void DestroyBase()
    {
        GameManager.Instance.LevelsManager.playerBaseList.Remove(this.gameObject);
        playerBaseAnimator.PlayDieAnimation();
        Destroy(gameObject, playerBaseAnimator.DieAnimationLength * 2);
    }

    [ContextMenu("Debug - TakeDamage")]
    public void DebugTakeDamage()
    {
        TakeDamage(10);
    }
}
