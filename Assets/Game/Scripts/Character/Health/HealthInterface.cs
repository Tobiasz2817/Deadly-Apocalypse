using UnityEngine;
using UnityEngine.UI;

public class HealthInterface : MonoBehaviour
{
    [SerializeField] private Image health;
    [SerializeField] private Health iGetHealth;

    private void OnEnable() {
        iGetHealth.OnNetworkHealthValueChange.AddListener(UpdateHealth);
    }

    private void OnDisable() {
        iGetHealth.OnNetworkHealthValueChange.RemoveListener(UpdateHealth);
    }
    
    private void UpdateHealth(float newHealth) {
        health.fillAmount = iGetHealth.GetHealth() / iGetHealth.GetMaxHealth();
    }
}
