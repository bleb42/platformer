using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Health))]
public class PlayerVampirizm : MonoBehaviour
{
    [SerializeField] private float _healthToTake = 5f;
    [SerializeField] private float _takingHealthSpeed = 1.0f;
    [SerializeField] private int _duration = 6;

    private Coroutine _takingHealth;
    private Health _health;
    private bool _isStarted = false;

    private void Awake()
    {
        _health = GetComponent<Health>();
    }

    public void StartTakingDamage(Health enemy)
    {
        if (_isStarted)
            return;

        _takingHealth = StartCoroutine(Vampirizm(enemy));
        _isStarted = true;
    }

    private IEnumerator Vampirizm(Health enemy)
    {
        WaitForSeconds secondsToWait = new WaitForSeconds(_takingHealthSpeed);
        int currentTime = 0; 

        for (int i = 0; i < _duration; i++)
        {
            currentTime++;

            enemy.TakeDamage(_healthToTake);
            _health.Heal(_healthToTake);

            yield return secondsToWait;
        }

        StopCoroutine(_takingHealth);
        _isStarted = false;
    }
}
