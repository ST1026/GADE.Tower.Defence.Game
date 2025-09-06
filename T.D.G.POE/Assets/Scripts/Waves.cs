using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waves : MonoBehaviour
{
    public GameObject enemies;
    public float waveInterval = 5f;
    public float spwnRate = 0.5f;

    //Paths for enemies
    public List<Vector3> path1;
    public List<Vector3> path2;
    public List<Vector3> path3;

    public int waveIndex = 0;

    void Start()
    {
        //Start First Wave
        
    }

    IEnumerator StartWave()
    {
        waveIndex++;
        Debug.Log ("Starting Wave " + waveIndex);

        //determine number of enemies in wave
        int enemycount = waveIndex * 5;

        for (int i = 0; i < enemycount; i++)
        {
            //have enemies select one of the three paths randomly
            int chosenPath = Random.Range(1, 4);
            List<Vector3> selectedPath = Path(chosenPath);

            GameObject Enemy = Instantiate(enemies, selectedPath[0], Quaternion.identity);
            EnemyMovement enemymovement = Enemy.GetComponent<EnemyMovement>();
            
            if (enemymovement != null)
            {
                enemymovement.SetPathways(selectedPath);
            }

            //wait period before another enemy spawns
            yield return new WaitForSeconds(spwnRate);

        }

        //wait period for next wave
        yield return new WaitForSeconds(waveInterval);
        StartCoroutine(StartWave());




    }

    //function to return correct path
    private List <Vector3> Path (int pathChosen)
    {
        switch (pathChosen)
        {
            case 1:
                return path1;
            case 2:
                return path2;
            case 3:
                return path3;
            default:
                return path1;

        }
    }






}
