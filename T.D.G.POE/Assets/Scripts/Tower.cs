using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tower : MonoBehaviour
{
    
    public float rof = 1f;
    public GameObject projectile;
    public Transform PointofFire;

    private float FireTime;

    
    void Update()
    {
        if (Time.time >= FireTime)
        {
            
            GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

            if (enemies.Length > 0)
            {
                //tower will shoot the closest enemy to it
                GameObject closestEnemy = GetClosestEnemy(enemies);

                if (closestEnemy != null)
                {
                    //aim at enemy
                    transform.LookAt(closestEnemy.transform);
                    Shoot(closestEnemy.transform);

                }
            }

        }
        
    }

    GameObject GetClosestEnemy(GameObject[] enemies)
    {
        //find the closest enemy in an array of enemies during waves
        GameObject cEnemy = null;
        float mDistance = Mathf.Infinity;
        Vector3 position = transform.position;

        foreach(GameObject enemy in enemies)
        {
            float distance = Vector3.Distance(enemy.transform.position, position);
            if (distance < mDistance)
            {
                cEnemy = enemy;
                mDistance = distance;

            }
        }
        return cEnemy;

    }

    void Shoot(Transform target)
    {
        //instantiate bullet and set an enemy as a target

        if (projectile != null && PointofFire != null)
        {
            GameObject bullet = Instantiate(projectile, PointofFire.position, PointofFire.rotation);
            
            Projectile bulletscript = bullet.GetComponent<Projectile>();
            if (bulletscript != null)
            {
                bulletscript.SetTarget(target);
            }
        }
    }
}
