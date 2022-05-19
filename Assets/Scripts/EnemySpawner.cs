using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public GameObject[] Enemy;

    private GameObject player;

    private float time = 0f;

    [SerializeField]
    private float newSpawnTime = 30f;

    [SerializeField]
    private float startingCount = 3f;

    [SerializeField]
    private float minDistance = 15f;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        for(int i = 0; i < startingCount; i++) SpawnEnemy();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;

        if( time >= newSpawnTime)
        {
            SpawnEnemy();
            time = 0f;
        }
       
    }

    private void SpawnEnemy()
    {
        Transform chosenSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        while (Vector3.Distance(chosenSpawnPoint.position,player.transform.position) < minDistance)
        {
            chosenSpawnPoint = SpawnPoints[Random.Range(0, SpawnPoints.Length)].transform;
        }
        GameObject chosenEnemy = Enemy[Random.Range(0, Enemy.Length)];
        Instantiate(chosenEnemy, chosenSpawnPoint.position,Quaternion.identity);
    }

    public float GetSpawnTime() { return newSpawnTime; }
}
