using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageCollider : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        EnemyState estates = other.transform.GetComponentInParent<EnemyState>();

        if (estates == null)
            return;

        estates.DoDamage(5);

    }
}
