using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        int goblinCount = enemycount;
        int fastGCount = 0;
        int bruteCount = 0;

        if (waveIndex >= 2)
        {
            fastGCount = enemycount / 3;
            goblinCount = enemycount;
        }



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
