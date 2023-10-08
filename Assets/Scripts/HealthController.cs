using UnityEngine;

public class HealthController : MonoBehaviour
{
    [SerializeField] private float _health;

    public float Health => _health;

    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float heal)
    {
        _health += heal;

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
