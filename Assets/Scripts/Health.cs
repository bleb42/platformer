using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public float HealthPoints => _health;

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
