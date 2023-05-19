using UnityEngine;

public class BasicHealth : Health
{
    [SerializeField] private float maxHealth = 100;
    [SerializeField] private float currentHealth = 100;
    
    public override float GetHealth() => currentHealth;
    public override float GetMaxHealth() => maxHealth;

    public override void ExpandMaxHealth(float newMaxHealth) {
        this.maxHealth = newMaxHealth;
        OnMaxHealthUpdate?.Invoke(newMaxHealth);
    }

    public override void ExpandCurrentHealth(float addHealth) {
        currentHealth = Mathf.Clamp(addHealth + currentHealth,0, maxHealth);
        OnHealthUpdate?.Invoke(currentHealth);
    }

    public override void TakeDamage(float damage) {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);
        OnHealthUpdate?.Invoke(currentHealth);
    }
}
