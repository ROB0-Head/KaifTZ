using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private void OnTriggerEnter(Collider col)
    {
        if (col.TryGetComponent(out EnemyController enemy))
        {
            enemy.Die();
        }
        Destroy(gameObject);
    }
    
}
