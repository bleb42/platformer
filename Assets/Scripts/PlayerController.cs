using UnityEngine;

[RequireComponent(typeof(HealthController))]
[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _damage = 5f;

    private const string State = "State";

    private Rigidbody2D _playerRig2D;
    private SpriteRenderer _playerRenderer;
    private Animator _animator;
    private HealthController _healthController;
    private int _coinsCount = 0;
    private bool _isGrounded;

    private void Awake()
    {
        Cursor.visible = false;

        _playerRenderer = GetComponent<SpriteRenderer>();
        _playerRig2D = GetComponent<Rigidbody2D>();
        _animator = GetComponent<Animator>();
        _healthController= GetComponent<HealthController>();
    }

    private void Update()
    {
        if (_isGrounded) _animator.SetInteger(State, (int)States.Idle);

        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            if (_animator.GetInteger(State) != (int)States.Jump)
            {
                _animator.SetInteger(State, (int)States.Attack);
            }
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.collider.TryGetComponent<Enemy>(out Enemy enemy))
            {
                Damage(enemy);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        _isGrounded = true;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.TryGetComponent(out Coin coin))
        {
            _coinsCount += 1;
            coin.Collect();
        }
        
        if(collision.TryGetComponent(out FirstAidKit firstAidKit))
        {
            _healthController.Heal(firstAidKit.HealPoints);
            Destroy(firstAidKit.gameObject);
        }
    }

    public void TakeDamage(float damage)
    {
        _healthController.TakeDamage(damage);
    }

    private void Damage(Enemy enemy)
    {
        enemy.TakeDamage(_damage);
    }

    private void Run()
    {
        if (_isGrounded)
            _animator.SetInteger(State, (int)States.Run);

        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);

        _playerRenderer.flipX = direction.x < 0f;
    }

    private void Jump()
    {
        if (_isGrounded)
        {
            _playerRig2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            _isGrounded = false;
            _animator.SetInteger(State, (int)States.Jump);
        }
    }
}

public enum States
{ 
    Idle,
    Run,
    Jump,
    Attack
}