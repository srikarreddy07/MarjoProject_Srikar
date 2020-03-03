using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [Header("Spawn Variables")]
    [SerializeField] bool canSpawnEnemies;
    [SerializeField] float waitForNextSpawn = 1.5f;
    [SerializeField] int spawnAvaible;
    [SerializeField] int spawnLength = 25;
    [SerializeField] int currentEnemiesOnScreen;
    private int maxEnemiesOnScreen = 15;

    [Header("Spawn Object")]
    [SerializeField] GameObject enemyPrefab;

    [Header("Spawn Variables")]
    [SerializeField] Transform[] spawnTrans;
    [SerializeField] GameObject[] enemiesArray;
    private Queue<Transform> enemyQueue = new Queue<Transform>();

    void Start()
    {
        enemiesArray = new GameObject[spawnLength];

        for (int i = 0; i < spawnLength; i++)
        {
            enemiesArray[i] = Instantiate(enemyPrefab, Vector3.zero, Quaternion.identity) as GameObject;

            Transform ObjTrans = enemiesArray[i].GetComponent<Transform>();
            enemyQueue.Enqueue(ObjTrans);

            enemiesArray[i].SetActive(false);
        }

        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy ()
    {
        yield return new WaitForSeconds(waitForNextSpawn);

        if (canSpawnEnemies)
        {
            spawnAvaible = 0;

            foreach (GameObject enemy in enemiesArray)
            {
                if (!enemy.activeInHierarchy)
                    spawnAvaible++;
            }

            if (spawnAvaible > 0)
            {
                if (currentEnemiesOnScreen < maxEnemiesOnScreen)
                {
                    // Randomize between left & right spawn points
                    int index = Random.Range(0, spawnTrans.Length);

                    Transform TS = enemyQueue.Dequeue();

                    TS.gameObject.SetActive(true);
                    TS.position = spawnTrans[index].position;

                    EAttackerController scr = TS.GetComponent<EAttackerController>();

                    if (spawnTrans[index].name == "Left Spawner")
                        scr.direction = EAttackerController.Direction.Right;
                    else
                        scr.direction = EAttackerController.Direction.Left;


                    enemyQueue.Enqueue(TS);

                    currentEnemiesOnScreen++;
                }
            }
        }

        yield return new WaitForSeconds(waitForNextSpawn);
        StartCoroutine(SpawnEnemy());
    }
}
