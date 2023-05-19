using Unity.Netcode;
using UnityEngine.Events;

public abstract class Health : NetworkBehaviour 
{
    public UnityEvent<float> OnHealthUpdate;
    public UnityEvent<float> OnMaxHealthUpdate;
    public UnityEvent<float> OnNetworkHealthValueChange;
    public abstract float GetHealth();
    public abstract float GetMaxHealth();
    public abstract void ExpandMaxHealth(float newMaxHealth);
    public abstract void ExpandCurrentHealth(float addHealth);
    public abstract void TakeDamage(float damage);
}

/*public interface IGetHealth
{
    public float GetHealth();
    public float GetMaxHealth();
}

public interface IAttackHealth
{
    public void TakeDamage(float damage);
}

public interface IExpandHealth
{
    public void ExpandMaxHealth(float newMaxHealth);
    public void ExpandCurrentHealth(float addHealth);
}*/