using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Health))]
public class HealthBar : MonoBehaviour
{
    [SerializeField] private Slider _healthBar;

    private Health _health;

    private void Start()
    {
        _health = GetComponent<Health>();
    }

    public void UpdateValue()
    {
        _healthBar.value = _health.HealthPoints;
    }
}
