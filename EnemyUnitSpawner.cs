using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyUnitSpawner : MonoBehaviour
{
    [SerializeField] float rangeAwayFromPlayer;
    [SerializeField] float spawnInterval;
    public List<NewEnemyData> enemyUnits = new List<NewEnemyData>();
    [SerializeField] NewEnemyData motherShip;
    private Vector2 playerPos;
    private bool enemysSpawning = false;
    private bool motherShipSpawned = false;
    public bool isMotherShip;

    // Update is called once per frame
    void FixedUpdate()
    {
        if (FindObjectOfType<PlayerController>() && !isMotherShip)
        {
            playerPos = FindObjectOfType<PlayerController>().gameObject.transform.position;
        }
        else playerPos = this.transform.position;
        if (playerPos != null && !enemysSpawning)
        {
            StartCoroutine(SpawnTimer());
            StartCoroutine(Spawn());
        }
        if(playerPos != null && !motherShipSpawned && motherShip != null)
        {
            StartCoroutine(SpawnMotherShipTimer());
        }
    }

    private IEnumerator Spawn()
    {
        while(spawnInterval >= 1f)
        {
            yield return new WaitForSeconds(60);
            spawnInterval -= 1f;
        }     
    }

    private IEnumerator SpawnTimer()
    {
        while (true)
        {
            enemysSpawning = true;
            SpawnEnemy();
            yield return new WaitForSeconds(spawnInterval);
            enemysSpawning = false;
        }
    }

    private IEnumerator SpawnMotherShipTimer()
    {
        while (true)
        {
            motherShipSpawned = true;
            yield return new WaitForSeconds(3000);
            SpawnMotherShip();
            motherShipSpawned = false;
        }
    }

    private void SpawnEnemy()
    {
        int i = Random.Range(0, enemyUnits.Count - 1);
        GameObject enemy = Instantiate(enemyUnits[i].obj, SpawnPos(playerPos, rangeAwayFromPlayer), Quaternion.identity);
        enemy.GetComponent<Health>().Initialize(enemyUnits[i]);
    }

    private void SpawnMotherShip()
    {
        GameObject enemy = Instantiate(motherShip.obj, SpawnPos(playerPos, 15), Quaternion.identity);
        enemy.GetComponent<Health>().Initialize(motherShip);
    }

    private Vector2 SpawnPos(Vector2 playerPos, float radius)
    {
        float randomAndle = Random.Range(0, 2f * Mathf.PI);
        float distance = radius + Random.Range(1f, 5f);

        float x = playerPos.x + Mathf.Cos(randomAndle) * distance;
        float y = playerPos.y + Mathf.Sin(randomAndle) * distance;

        return new Vector2(x, y);
    }
}
