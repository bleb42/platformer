using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    [SerializeField] private float _speed = 3f;
    [SerializeField] private float _jumpForce = 3f;
    [SerializeField] private float _damage = 5f;
    [SerializeField] private float _interactionRadius = 2f;
    
    public float InteractionRadius { get; private set; }
    public bool IsGrounded { get; private set; }

    private Rigidbody2D _playerRig2D;

    private void Awake()
    {
        Cursor.visible = false;
        IsGrounded = false;
        InteractionRadius = _interactionRadius;

        _playerRig2D = GetComponent<Rigidbody2D>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IsGrounded = true;
    }

    public void Damage(Health enemy)
    {
        enemy.TakeDamage(_damage);
    }

    public void Run(float horizontalInput)
    {
        Vector3 direction = transform.right * horizontalInput;

        transform.position =
            Vector3.MoveTowards(transform.position, transform.position + direction, _speed * Time.deltaTime);
    }

    public void Jump()
    {
        if (IsGrounded)
        {
            _playerRig2D.AddForce(transform.up * _jumpForce, ForceMode2D.Impulse);
            IsGrounded = false;
        }
    }
}