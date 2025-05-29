using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    [SerializeField] private GameObject player;
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject enemy;
    [SerializeField] private float spawnInterval = 1f;

    [SerializeField] private Camera cam;
    [SerializeField] private float mapWidthOffset = 0f;
    [SerializeField] private float mapHeightOffset = 0f;
    [SerializeField] private int maxEnemies = 8;

    private List<GameObject> spawnedEnemies = new List<GameObject>();

    private void OnEnable()
    {
        StartCoroutine(SpawnEnemy());
    }

    IEnumerator SpawnEnemy()
    {
        if (spawnedEnemies.Count >= maxEnemies) {yield return null; }
        while (true)
        {
            if (spawnedEnemies.Count > maxEnemies)
            {
                Destroy(spawnedEnemies[0]);
                spawnedEnemies.RemoveAt(0);

                //yield return new WaitForSeconds(spawnInterval);
                //continue;  // skip this iteration, don't spawn new enemy
            }

            Vector2 randomPosition;

            do
            {
                float mapHeight = 2f * cam.orthographicSize;
                float mapWidth = mapHeight * cam.aspect;

                randomPosition = new Vector2(
                Random.Range((-mapWidth / 2) - mapWidthOffset, (mapWidth / 2) + mapWidthOffset),
                Random.Range((-mapHeight / 2) - mapHeightOffset, (mapHeight / 2) + mapHeightOffset));
            }
            while (Vector2.Distance(randomPosition, player.transform.position) <= radius);

            transform.position = randomPosition;
            GameObject newEnemy = Instantiate(enemy, transform.position, Quaternion.identity);

            spawnedEnemies.Add(newEnemy);

            yield return new WaitForSeconds(spawnInterval);

        }
    }

    public void RemoveEnemy(GameObject enemy)
    {
        if (spawnedEnemies.Contains(enemy))
        {
            spawnedEnemies.Remove(enemy);
        }
    }

    private void OnDisable()
    {
        StopCoroutine(SpawnEnemy());
        if (this.gameObject.scene.isLoaded)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                Destroy(spawnedEnemies[i]);
            }
            spawnedEnemies.Clear();
        }
    }

    private void OnDestroy()
    {
        StopCoroutine(SpawnEnemy());
        if (this.gameObject.scene.isLoaded)
        {
            for (int i = 0; i < spawnedEnemies.Count; i++)
            {
                Destroy(spawnedEnemies[i]);
            }
            spawnedEnemies.Clear();
        }
    }
}
