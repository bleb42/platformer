using UnityEngine;

public class Camera : MonoBehaviour
{
    [SerializeField] private Transform _target;
    [SerializeField] private Vector3 _position;

    private void Awake()
    {
        _position = transform.position;
    }

    private void Update()
    { 
        _position.x = _target.position.x;
        transform.position = _position;
    }
}
