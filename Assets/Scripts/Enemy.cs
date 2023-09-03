using UnityEngine;

[RequireComponent(typeof(Animator))]
public class Enemy : MonoBehaviour
{
    [SerializeField] Transform[] _moveSpots;
    [SerializeField] private float _speed;

    private int _randomSpot;

    private void Awake()
    {
        _randomSpot = Random.Range(0, _moveSpots.Length);
    }

    private void Update()
    {
        transform.position
            = Vector3.MoveTowards(transform.position, _moveSpots[_randomSpot].position, _speed * Time.deltaTime);

        if (Vector2.Distance(transform.position, _moveSpots[_randomSpot].position) < 0.5f)
        {
            _randomSpot = Random.Range(0, _moveSpots.Length);
        }
    }
}
