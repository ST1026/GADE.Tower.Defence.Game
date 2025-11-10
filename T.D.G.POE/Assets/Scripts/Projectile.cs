using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 5f;
    public int bulletdamage;
    
    //projectile target
    private Transform target;

    void Start()
    {
       
    }

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
        transform.LookAt(target);
        transform.Translate(direction.normalized * distance, Space.World);
    }

    void OnCollisionEnter(Collision collision)
    {
        
        if (collision.collider.CompareTag("Enemy"))
        {
            EnemyHealth enemyHealth = collision.gameObject.GetComponent<EnemyHealth>();

            //check if object hit is an enemy then deals damage
            if(enemyHealth != null)
            {
                enemyHealth.Damage(bulletdamage);
            }
        }
        Destroy(gameObject);
        
    }


}
