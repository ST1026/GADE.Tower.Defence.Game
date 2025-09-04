using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed = 5f;

    private List<Vector3> pathway;
    private int currentWaypoint = 0;

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
        
    }
}
