using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class Spawner : MonoBehaviour
{
    [Header("Settings")]
    [SerializeField] private GameObject player;
    [SerializeField] private float radius = 0f;

    [SerializeField] private GameObject enemy;
    [SerializeField] private GameObject enemyRanged;
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
    private void Update()
    {
        Debug.Log(spawnedEnemies.Count); // count decreases when enemies are killed, increases when enemies are spawned
    }

    IEnumerator SpawnEnemy()
    {
        while (true)
        {
            if (spawnedEnemies.Count < maxEnemies)
            {
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

                GameObject enemyType = Random.Range(0, 2) == 0 ? enemy : enemyRanged;
                GameObject newEnemy = Instantiate(enemyType, transform.position, Quaternion.identity);
                spawnedEnemies.Add(newEnemy);
            }

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
