using UnityEngine;

[RequireComponent(typeof(PlayerCollector))]
[RequireComponent(typeof(Health))]
[RequireComponent(typeof(PlayerAnimations))]
[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _damage = 5f;
    
    public bool IsGrounded { get; private set; }

    private Rigidbody2D _playerRig2D;

    private void Awake()
    {
        Cursor.visible = false;
        IsGrounded = false;

        _playerRig2D = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        if (Input.GetButton("Horizontal"))
            Run();

        if (Input.GetKeyDown(KeyCode.Space))
            Jump();
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            if (collision.collider.TryGetComponent(out HealthController enemy))
            {
                Damage(enemy);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    private void Damage(HealthController enemy)
    {
        enemy.TakeDamage(_damage);
    }

    private void Run()
    {
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");

        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    private void Jump()
    {
        if (IsGrounded)
        {
            _playerRig2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            IsGrounded = false;
        }
    }
}
