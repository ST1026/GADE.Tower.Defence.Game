using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    public float atkRange = 10f;
    public float rof = 1f;
    public GameObject projectile;
    public Transform PointofFire;

    private float FireTime;
    private List <EnemyMovement> enemyinRange = new List <EnemyMovement>();

   
    void Start()
    {
        //setting collider's radius to attack range
        SphereCollider collider = gameObject.AddComponent<SphereCollider>();
        collider.isTrigger = true;
        collider.radius = atkRange;

    }

    
    void Update()
    {
        //Checking if enemies are in range to fire
        if (enemyinRange.Count > 0 && Time.time >= FireTime)
        {
            //targetting
            EnemyMovement target = enemyinRange[0];

            if (target != null)
            {
                Shoot(target.transform);
                FireTime = Time.time + 1 / rof;
            }
            else
            {
                //Remove null enemies from list
                enemyinRange.RemoveAt(0);
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        EnemyMovement enemy = other.GetComponent<EnemyMovement>();
        if (enemy != null && enemyinRange.Contains(enemy))
        {
            enemyinRange.Remove(enemy);
        } 
    }

    void Shoot(Transform target)
    {
        if (projectile != null && PointofFire != null)
        {
            GameObject bullet = Instantiate(projectile, PointofFire.position, PointofFire.rotation);
            bullet.GetComponent<Projectile>().SetTarget(target);
        }
    }

}
