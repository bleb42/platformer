using System.Collections;
using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(SpriteRenderer))]
public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] _moveSpots;
    [SerializeField] private float _speed;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _attackSpeed = 10f;
    
    private Coroutine _attack;
    private Coroutine _patrol;

    private SpriteRenderer _spriteRenderer;
    private HealthController _healthController;
    private GameObject _targetToChase;
    private int _randomSpot;
    private float _distanceToSpot = 0.5f;
    private bool _isPatrolling = true;
    private bool _isChasing = false;   

    private void Awake()
    {
        _randomSpot = Random.Range(0, _moveSpots.Length);
        _spriteRenderer = GetComponent<SpriteRenderer>();
        _healthController = GetComponent<HealthController>();
        _patrol = StartCoroutine(Patrol());
    }

    private void Update()
    {
        if (_isPatrolling)
        {
            Patrol();
        }
        else if (_isChasing) 
        {
            _spriteRenderer.flipX = _targetToChase.transform.position.x < transform.position.x;

            transform.position = Vector3.Lerp(transform.position, _targetToChase.transform.position, Time.deltaTime * _speed);
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (_patrol != null)
            {
                StopCoroutine(_patrol);
            }

            _isChasing = true;
            _isPatrolling = false;
            _targetToChase = player.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _patrol = StartCoroutine(Patrol());

            _targetToChase = null;

            _isChasing = false;
            _isPatrolling = true;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (_attack == null)
            {
                _attack = StartCoroutine(Damage(player));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            StopCoroutine(_attack);

            _attack = null;
        }
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
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

    private IEnumerator Damage(PlayerController player)
    {
        WaitForSeconds attackSpeed = new WaitForSeconds(_attackSpeed);

        while(true)
        {
            player.TakeDamage(_damage);
            
            yield return attackSpeed;
        }
    }
}
