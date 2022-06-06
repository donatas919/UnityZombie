using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using Random = UnityEngine.Random;

public class EnemySpawner : MonoBehaviour
{
    public GameObject enemyPrefab;
    public float spawnRate = 2f;

    private Vector2 whereToSpawn;
    private float randX;
    private float randY;
    private float nextSpawn = 0.0f;

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextSpawn)
        {
            nextSpawn = Time.time + spawnRate;
            randX = Random.Range(-40f, 36f);
            randY = Random.Range(-32f, 24f);
            whereToSpawn = new Vector2(randX, randY);
            GameObject enemy = Instantiate(enemyPrefab, whereToSpawn, quaternion.identity) as GameObject;
        }
    }
}
