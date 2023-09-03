using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private float _folowingSpeed;

    private Vector3 _position;

    private void Awake()
    {
        _position = transform.position;
    }

    private void Update()
    {
        _position = _target.position;
        _position.z = -10f;
        _position.y = 0;

        transform.position = Vector3.Lerp(transform.position, _position, Time.deltaTime * _folowingSpeed);
    }
}
