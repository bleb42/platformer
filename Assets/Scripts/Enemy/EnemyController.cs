using System.Collections;
using UnityEngine;

[RequireComponent(typeof(EnemyTrigger))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class EnemyController : MonoBehaviour
{
    [SerializeField] Transform[] _moveSpots;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackSpeed = 10f;

    private Coroutine _patrol;

    private EnemyTrigger _enemyTrigger;
    private SpriteRenderer _spriteRenderer;
    private int _randomSpot;
    private float _distanceToSpot = 0.5f;
    private bool _isPatrolling = true;
    private bool _isChasing = false;

    private void Awake()
    {
        _randomSpot = Random.Range(0, _moveSpots.Length);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _patrol = StartCoroutine(Patrol());
        _enemyTrigger = GetComponent<EnemyTrigger>();
    }

    private void Update()
    {
        if (_isPatrolling)
        {
            Patrol();
        }
        else if (_isChasing)
        {
            _spriteRenderer.flipX = _enemyTrigger.TargetToChase.transform.position.x < transform.position.x;

            transform.position = 
                Vector3.Lerp(transform.position, _enemyTrigger.TargetToChase.transform.position, Time.deltaTime * _speed);
        }
    }

    public void StartChaising()
    {
        if (_patrol != null)
        {
            StopCoroutine(_patrol);
        }

        _isChasing = true;
        _isPatrolling = false;
    }

    public void StartPatrol()
    {
        if (_patrol != null)
        {
            _patrol = StartCoroutine(Patrol());
        }

        _isChasing = false;
        _isPatrolling = true;
    }

    public IEnumerator Damage(Health player)
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(_attackSpeed);

        while (true)
        {
            player.TakeDamage(_damage);

            yield return attackSpeed;
        }
    }

    private IEnumerator Patrol()
    {
        while (_isPatrolling)
        {
            transform.position
                = Vector3.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < _distanceToSpot)
            {
                _randomSpot = Random.Range(0, _moveSpots.Length);

                _spriteRenderer.flipX = _moveSpots[_randomSpot].position.x < transform.position.x;
            }

            yield return null;
        }
    }
}