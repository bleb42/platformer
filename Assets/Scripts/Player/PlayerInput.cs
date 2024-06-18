using UnityEngine;

[RequireComponent(typeof(PlayerMovement), typeof(PlayerVampirizm))]
public class PlayerInput : MonoBehaviour
{
    private const string Horizontal = "Horizontal";

    private PlayerMovement _playerMovement;
    private PlayerVampirizm _playerVampirizm;
    private float _horizontalInput;

    private void Awake()
    {
        _playerMovement = GetComponent<PlayerMovement>();
        _playerVampirizm = GetComponent<PlayerVampirizm>();
    }

    private void Update()
    {
        _horizontalInput = Input.GetAxis(Horizontal);

        if (_horizontalInput != 0)
            _playerMovement.Run(_horizontalInput);

        if (Input.GetKeyDown(KeyCode.Space))
            _playerMovement.Jump();

        if (Input.GetKeyDown(KeyCode.E))
        {
            Collider2D[] hits = Physics2D.OverlapCircleAll(gameObject.transform.position, _playerMovement.InteractionRadius);

            foreach (var hit in hits) 
            {
                if (hit.TryGetComponent(out EnemyController enemy))
                {
                    _playerMovement.Damage(enemy.GetComponent<Health>());
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            _playerVampirizm.StartVampirizm();
        }
    }
}
