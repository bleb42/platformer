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

    private EnemyTrigger _enemyTrigger;
    private SpriteRenderer _spriteRenderer;
    private int _randomSpot;
    private float _distanceToSpot = 0.5f;
    private bool _isPatrolling;
    private bool _isChasing;

    private void Awake()
    {
        _randomSpot = Random.Range(0, _moveSpots.Length);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _enemyTrigger = GetComponent<EnemyTrigger>();
    }

    private void Start()
    {
        StartPatrol();
    }

    private void Update()
    {
        if (_isPatrolling)
        {
            transform.position
                = Vector3.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);

            if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < _distanceToSpot)
            {
                _randomSpot = Random.Range(0, _moveSpots.Length);

                _spriteRenderer.flipX = _moveSpots[_randomSpot].position.x < transform.position.x;
            }
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
        _isChasing = true;
        _isPatrolling = false;
    }

    public void StartPatrol()
    {
        _spriteRenderer.flipX = _moveSpots[_randomSpot].position.x < transform.position.x;

        _isChasing = false;
        _isPatrolling = true;
    }

    public IEnumerator ApplyDamage(Health player)
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(_attackSpeed);

        while (true)
        {
            player.TakeDamage(_damage);

            yield return attackSpeed;
        }
    }
}