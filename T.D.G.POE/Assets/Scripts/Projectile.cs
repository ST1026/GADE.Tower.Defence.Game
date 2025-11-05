using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int bulletdamage;
    //projectile target
    private Transform target;

    public void BulletStats(Transform newTarget, int dmgvalue)
    {
        target = newTarget;
        bulletdamage = dmgvalue;
    }

    void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        //ensure bullet travels to enemy's position
        Vector3 direction = target.position - transform.position;
        float distance = speed * Time.deltaTime;
        transform.Translate(direction.normalized * distance, Space.World);
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Enemy"))
        {
            
            //check if object hit is an enemy then deals damage
            if (other.TryGetComponent<EnemyHealth>(out EnemyHealth enemyHealth))
            {
                enemyHealth.Damage(bulletdamage);
            }
        }
        Destroy(gameObject);
        
    }


}
