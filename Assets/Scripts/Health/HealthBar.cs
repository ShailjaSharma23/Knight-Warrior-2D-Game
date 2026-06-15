using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] private Health health;
    [SerializeField] private Image totalHealth;
    [SerializeField] private Image currentHealth;

    private void Start()
    {
        totalHealth.fillAmount = health.currentHealth / 10;
    }

    private void Update()
    {
        currentHealth.fillAmount = health.currentHealth / 10;
    }
}
