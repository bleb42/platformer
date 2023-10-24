using UnityEngine;

public class FirstAidKit : MonoBehaviour
{
    [SerializeField] private float _healPoints = 20;

    public float HealPoints => _healPoints;
}
