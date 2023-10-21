using UnityEngine;

[RequireComponent(typeof(HealthController))]
public class PlayerCollector : MonoBehaviour
{
    private Health _healthController;
    private int _coinsCount = 0;

    private void Start()
    {
        _healthController = GetComponent<Health>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinsCount += 1;
            coin.Collect();
        }

        if (collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            _healthController.Heal(firstAidKit.HealPoints);
            Destroy(firstAidKit.gameObject);
        }
    }
}
