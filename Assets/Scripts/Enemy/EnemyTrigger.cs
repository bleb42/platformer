using UnityEngine;

[RequireComponent(typeof(EnemyController))]
public class EnemyTrigger : MonoBehaviour
{
    public GameObject TargetToChase { get; private set; }
    public Coroutine Attack { get; private set; }

    private EnemyController _enemyController;

    private void Start()
    {
        _enemyController = GetComponent<EnemyController>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _enemyController.StartChaising();

            TargetToChase = player.gameObject;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            _enemyController.StartPatrol();

            TargetToChase = null;
        }
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out Health playerHealth) && collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (Attack == null)
            {
                Attack = StartCoroutine(_enemyController.Damage(playerHealth));
            }
        }
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.TryGetComponent(out PlayerController player))
        {
            if (Attack != null)
                StopCoroutine(Attack);

            Attack = null;
        }
    }
}
