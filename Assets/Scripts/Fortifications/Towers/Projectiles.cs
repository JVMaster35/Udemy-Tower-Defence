using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectiles : MonoBehaviour
{

    public Rigidbody rBDY;

    public float fireSpeed;
    public float damage;

    public GameObject impactEffect;

    public bool hasBurst;
    private bool hasDamaged;

    // Start is called before the first frame update
    void Start()
    {
        rBDY = GetComponent<Rigidbody>();
        rBDY.linearVelocity = transform.forward * fireSpeed;

        AudioManager.instance.PlaySFX(2);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.CompareTag("Enemy") && !hasDamaged)
        {
            other.GetComponent<EnemyHealthController>().TakeDamage(damage);
            if(!hasBurst)
            {
                hasDamaged = true;
            }
        }

        Instantiate(impactEffect, transform.position, transform.rotation);
        AudioManager.instance.PlaySFX(6);
        Destroy(gameObject);
    }

    private void OnBecameInvisible()
    {
        Destroy(gameObject);
    }
}
