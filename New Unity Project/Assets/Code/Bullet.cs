using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject hitEffect;

    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(hitEffect, transform.position, Quaternion.identity);
        Destroy(effect, 0.5f);
        Destroy(gameObject);
        
        ITakeDamage damagable = collision.collider.GetComponent<ITakeDamage>();
        if (damagable != null)
            damagable.TakeDamage(1);
    }
}
