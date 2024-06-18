using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyTrigger : MonoBehaviour
{
    public GameObject TargetToChase { get; private set; }
    public Coroutine Attack { get; private set; }

    private EnemyController _enemyController;

    private void Awake()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            _enemyController.StartChaising();

            TargetToChase = player.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            _enemyController.StartPatrol();

            TargetToChase = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health playerHealth) && collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            if (Attack == null)
            {
                Attack = StartCoroutine(_enemyController.ApplyDamage(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerMovement player))
        {
            if (Attack != null)
                StopCoroutine(Attack);

            Attack = null;
        }
    }
}
