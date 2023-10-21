using UnityEngine;

[RequireComponent(typeof(Health))]
public class HealthController : MonoBehaviour
{
    private Health _healthController;

    private void Start()
    {
        _healthController = GetComponent<Health>();
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
    }
}
