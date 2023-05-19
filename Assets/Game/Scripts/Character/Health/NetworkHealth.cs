using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class NetworkHealth : Health
{
    private NetworkVariable<float> currentHealth = new NetworkVariable<float>(100,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);
    private NetworkVariable<float> maxHealth = new NetworkVariable<float>(100,NetworkVariableReadPermission.Everyone,NetworkVariableWritePermission.Owner);

    public override float GetHealth() => currentHealth.Value;
    public override float GetMaxHealth() => maxHealth.Value;
    

    public override void OnNetworkSpawn() {
        currentHealth.OnValueChanged += ValueChange;
    }

    public override void OnNetworkDespawn() {
        currentHealth.OnValueChanged -= ValueChange;
    }
    
    private void ValueChange(float previousValue, float newValue) {
        OnNetworkHealthValueChange.Invoke(newValue);
    }

    public override void ExpandMaxHealth(float newMaxHealth) {
        this.maxHealth.Value = newMaxHealth;
        OnMaxHealthUpdate?.Invoke(newMaxHealth);
    }

    public override void ExpandCurrentHealth(float addHealth) {
        currentHealth.Value = Mathf.Clamp(addHealth + currentHealth.Value,0, maxHealth.Value);
        OnHealthUpdate?.Invoke(currentHealth.Value);
    }

    public override void TakeDamage(float damage) {
        currentHealth.Value = Mathf.Clamp(currentHealth.Value - damage, 0, maxHealth.Value);
        OnHealthUpdate?.Invoke(currentHealth.Value);
    }
}
