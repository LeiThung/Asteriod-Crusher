using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class AsteroidsSpawner : MonoBehaviour
{
    [SerializeField] float rangeAwayFromPlayer;
    [SerializeField] float spawnInterval;
    public List<NewEnemyData> asteroids = new List<NewEnemyData>();
    private Vector2 playerPos;
    private bool enemysSpawning = false;
    [SerializeField] float healthMultipliar = 0.5f;
    [SerializeField] int secondsForMoreHealth = 60;
    private float multipliar = 1;

    private void Start()
    {
        StartCoroutine(AddHealth());
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(FindObjectOfType<PlayerController>())
        {
            playerPos = FindObjectOfType<PlayerController>().gameObject.transform.position;
        }
        if(playerPos != null && !enemysSpawning) StartCoroutine(SpawnTimer());
    }

    private IEnumerator SpawnTimer()
    {
        while(true)
        {
            enemysSpawning = true;
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            enemysSpawning = false;
        } 
    }

    private IEnumerator AddHealth()
    {
        while(true)
        {
            yield return new WaitForSeconds(secondsForMoreHealth);
            multipliar += healthMultipliar;           
        }
    } 

    private void SpawnEnemy()
    {
        int i = UnityEngine.Random.Range(0, asteroids.Count - 1);
        GameObject enemy = Instantiate(asteroids[i].obj, SpawnPos(playerPos, rangeAwayFromPlayer), Quaternion.identity);
        enemy.GetComponent<Health>().Initialize(asteroids[i]);
        enemy.GetComponent<Health>().health *= multipliar;
    }

    private Vector2 SpawnPos(Vector2 playerPos, float radius)
    {
        float randomAndle = UnityEngine.Random.Range(0, 2f * Mathf.PI);
        float distance = radius + UnityEngine.Random.Range(1f, 5f);

        float x = playerPos.x + Mathf.Cos(randomAndle) * distance;
        float y = playerPos.y + Mathf.Sin(randomAndle) * distance;

        return new Vector2(x, y);
    }
}
