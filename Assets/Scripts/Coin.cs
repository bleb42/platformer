using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    public void Collect()
    {
        Destroy(gameObject);
    }
}
