using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    private List<Vector3> pathway;
    private int currentWaypoint = 0;

    public int Damage = 1;

    public void SetPathways(List<Vector3> newPathways)
    {
        pathway = newPathways;
        if (pathway.Count > 0 )
        {
            transform.position = pathway[0];
            currentWaypoint = 0;
        }
    }

    void Update()
    {
        if (pathway == null || pathway.Count == 0)
            return;

        if (currentWaypoint < pathway.Count)
        {
            Vector3 target = pathway[currentWaypoint];
            transform.position = Vector3.MoveTowards(transform.position, target, speed * Time.deltaTime);

            //move to next waypoint
            if (Vector3.Distance(transform.position, target) < speed)
            {
                currentWaypoint++;
            }
        }
        else
        {
            Debug.Log("Enemy has reached Tower");
            ReachedTower();
        }
        
    }

    private void ReachedTower()
    {
        //when the enemy reaches the tower they deal damage to the tower
        GameObject tower = GameObject.FindGameObjectWithTag("Player");
        if (tower != null)
        {
            TowerHealth towerHealth = tower.GetComponent<TowerHealth>();
            if (towerHealth != null)
            {
                towerHealth.DamageTaken(Damage);
            }

        }
    }


}
