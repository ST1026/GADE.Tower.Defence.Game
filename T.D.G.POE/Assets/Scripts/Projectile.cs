using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 1;

    //projectile target
    private Transform target;

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
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

        //check whether projectile misses the target
        if (direction.magnitude <= distance)
        {
            HitTarget();
            return;

        }

        transform.Translate(direction.normalized * distance, Space.World);

        //method called when projectile hits enemy
        void HitTarget()
        {
            EnemyHealth enemyHealth = target.GetComponent<EnemyHealth>();
            if (enemyHealth != null)
            {
                //deal damage to enemy
                enemyHealth.Damage(damage);
            }

            Destroy(gameObject);
        }


    }
}
