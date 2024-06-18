using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(SpriteRenderer), typeof(Animator))]
public class PlayerAnimations : MonoBehaviour
{
    private const string State = "State";

    private SpriteRenderer _playerRenderer;
    private Animator _animator;
    private PlayerMovement _playerController;

    private void Awake()
    {
        _playerRenderer= GetComponent<SpriteRenderer>();
        _animator= GetComponent<Animator>();
        _playerController= GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (_playerController.IsGrounded)
            _animator.SetInteger(State, (int)States.Idle);
        
        if (Input.GetButton("Horizontal"))
            if (_playerController.IsGrounded)
                _animator.SetInteger(State, (int)States.Run);

        if (Input.GetKeyDown(KeyCode.E))
            if (_animator.GetInteger(State) != (int)States.Jump)
                _animator.SetInteger(State, (int)States.Attack);

        if (Input.GetKeyDown(KeyCode.Space))
            _animator.SetInteger(State, (int)States.Jump);
        
        Vector3 direction = transform.right * Input.GetAxis("Horizontal");
        _playerRenderer.flipX = direction.x < 0f;
    }
}

public enum States
{
    Idle,
    Run,
    Jump,
    Attack
}