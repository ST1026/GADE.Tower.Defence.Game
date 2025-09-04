using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathways : MonoBehaviour
{
    public List<Vector3> GeneratePathway(Vector3 start, Vector3 end, Terrain terrain)
    {
        List<Vector3> pathways = new List<Vector3>();
        pathways.Add(start);
        pathways.Add(end);
        return pathways;
    }
}
