using UnityEngine;

[RequireComponent(typeof(HealthBar))]
public class Health : MonoBehaviour
{
    [SerializeField] private float _health;

    public float HealthPoints => _health;

    private HealthBar _healthBar;
    private float _maxHealth;

    private void Awake()
    {
        _maxHealth = _health;
    }

    private void Start()
    {
        _healthBar = GetComponent<HealthBar>();
    }

    public void TakeDamage(float damage)
    {
        _health -= damage;

        _healthBar.UpdateValue();

        if (_health <= 0)
        {
            Destroy(gameObject);
        }
    }

    public void Heal(float heal)
    {
        _health += heal;

        _healthBar.UpdateValue();

        if (_health > _maxHealth)
        {
            _health = _maxHealth;
        }
    }
}
