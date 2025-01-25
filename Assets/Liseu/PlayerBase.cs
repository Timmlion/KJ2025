using UnityEngine;

public class PlayerBase : MonoBehaviour
{

    public int health = 100;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void TakeDamage(int damageAmount) {
        health -= damageAmount;
        if (health <= 0) {
            DestroyBase();
            }
    }

    public void DestroyBase()
    {
        Destroy(gameObject);
    }
}
