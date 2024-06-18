using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health), typeof(PlayerMovement))]
public class PlayerVampirizm : MonoBehaviour
{
    [SerializeField] private float _healthToTake = 5f;
    [SerializeField] private float _takingHealthDelay = 1.0f;
    [SerializeField] private int _duration = 6;
    [SerializeField] private GameObject _vampirizmZone;
    [SerializeField] private VampirizmTimer _timer;

    private PlayerMovement _playerMovement;
    private Coroutine _takingHealth;
    private Health _health;
    private bool _isStarted = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
        _playerMovement= GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        _vampirizmZone.SetActive(false);
    }

    public void StartVampirizm()
    {
        if (_isStarted)
            return;

        _takingHealth = StartCoroutine(Vampirizm());
        _isStarted = true;
    }

    private IEnumerator Vampirizm()
    {
        _timer.gameObject.SetActive(true);
        _vampirizmZone.SetActive(true);
        WaitForSeconds takingHealthDelay = new WaitForSeconds(_takingHealthDelay);

        for (int i = 0; i < _duration; i++)
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, _playerMovement.InteractionRadius);
            _timer.UpdateValue(_duration - i);

            foreach (var hit in hits)
            {
                if (hit.TryGetComponent(out Health enemy))
                {
                    enemy.TakeDamage(_healthToTake);
                    _health.Heal(_healthToTake);
                }
            }
            
            yield return takingHealthDelay;
        }

        _isStarted = false;
        _vampirizmZone.SetActive(false);
        _timer.gameObject.SetActive(false);
    }
}
