using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Wave : MonoBehaviour
{
    [Header ("Enemy Prefabs")]
    public GameObject goblinPrefab;
    public GameObject fastPrefab;
    public GameObject brutePrefab;

    [Header ("Wave Settings")]
    public float waveInterval = 5f;
    public float spwnRate = 0.5f;

    [Header ("UI")]
    public TMP_Text waveText;
    public TMP_Text enemiesLeftTxt;
    public int waveIndex = 0;
    public int enemiesLeft = 0;


    [Header ("Paths")]
    public List<Vector3> path1;
    public List<Vector3> path2;
    public List<Vector3> path3;


    void Start()
    {
        //Start First Wave
        StartCoroutine(StartWave());
    }

    void Update()
    {
        waveText.text = "Wave: " + waveIndex.ToString();
        enemiesLeftTxt.text = "Enemies Left: " + enemiesLeft.ToString();
    }

    IEnumerator StartWave()
    {
        waveIndex++;
        Debug.Log ("Starting Wave " + waveIndex);

        //determine number of enemies in wave
        int enemycount = waveIndex * 5;

        //enemy distrubution
        int goblinCount = enemycount;
        int fastGCount = 0;
        int bruteCount = 0;

        //fast enemies to start spawning from waves 3 onwards
        if (waveIndex >= 3)
        {
            fastGCount = enemycount / 2;
            goblinCount = enemycount;
        }

        //brute enemy spawns from wave 4 onwards
        if (waveIndex >= 4)
        {
            bruteCount = enemycount / 4;
            fastGCount = (enemycount - bruteCount) / 2;
            goblinCount = enemycount;
        }

        enemiesLeft = enemycount;

        for (int i = 0; i < goblinCount; i++)
        {
            SpawnEnemies(goblinPrefab);
            yield return new WaitForSeconds(spwnRate);
        }

        for (int i = 0; i < fastGCount; i++)
        {
            SpawnEnemies(fastPrefab);
            yield return new WaitForSeconds(spwnRate);
        }

        for (int i = 0; i < bruteCount; i++)
        {
            SpawnEnemies(brutePrefab);
            yield return new WaitForSeconds(spwnRate);
        }

        while (enemiesLeft > 0)
        {
            yield return null;
        }

        //wait period for next wave
        yield return new WaitForSeconds(waveInterval);
        StartCoroutine(StartWave());
    }

    private void SpawnEnemies(GameObject enemyPrefab)
    {
        //enemies select one of three pathways to get to the tower at random
        int chosenPath = Random.Range(1, 4);
        List<Vector3> pathSelected = Path(chosenPath);

        GameObject Enemy = Instantiate(enemyPrefab, pathSelected[0], Quaternion.identity);
        EnemyMovement enemyMovement = Enemy.GetComponent<EnemyMovement>();

        if (enemyMovement != null)
        {
            enemyMovement.SetPathways(pathSelected);
        }
    }

    public void EnemyDefeated()
    {
        enemiesLeft--;
        Debug.Log("Enemies Remaining" + enemiesLeft);

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
