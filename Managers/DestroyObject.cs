using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyObject : MonoBehaviour
{
    [SerializeField] private float timer = 0;
    private void Start()
    {
        Destroy(gameObject, timer);
    }
}
