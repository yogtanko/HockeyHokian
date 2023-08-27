using UnityEngine;

public abstract class Player
{
    [SerializeField] protected float currentHealth;
    [SerializeField] protected float maxHealth;

    protected Player(float currentHealth,float maxHealth)
    {
        this.currentHealth = currentHealth;
        this.maxHealth = maxHealth;
    }
    protected virtual void SetCurrentHealth(float value)
    {
        this.currentHealth = value;
    }
    protected virtual void SetMaxHealth(float value)
    {
        this.maxHealth = value;
    }
    protected virtual float GetCurrentHealth()
    {
        return this.currentHealth;
    }
    protected virtual float GetMaxHealth()
    {
        return this.maxHealth;
    }
    protected virtual void Move(HH.Movement movement,GameObject gameObject) {
        movement.MoveTo();
    }
}