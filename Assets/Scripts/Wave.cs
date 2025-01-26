using UnityEngine;

public class Wave 
{
    public int amount;
    public ElementType type;
    public float speed;
    public int health;

    public Wave(int amount, ElementType type, float speed, int health)
    {
        this.amount = amount;
        this.type = type;
        this.speed = speed;
        this.health = health;
    }
}
