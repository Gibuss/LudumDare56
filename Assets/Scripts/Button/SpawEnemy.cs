using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private GameObject lifeBarPrefab;
    [SerializeField] private GameObject spawnPointPrefab;
    [SerializeField] private Canvas canvas;

    private Transform spawnPoint;

    public void SpawnEnemy()
    {
        if (spawnPoint == null)
        {
            spawnPoint = Instantiate(spawnPointPrefab, canvas.transform).transform;
        }

        GameObject enemy = Instantiate(enemyPrefab, canvas.transform);

        RectTransform enemyRect = enemy.GetComponent<RectTransform>();
        if (enemyRect != null && spawnPoint != null)
        {
            enemyRect.anchoredPosition = spawnPoint.GetComponent<RectTransform>().anchoredPosition;
        }
        else
        {
            enemy.transform.position = spawnPoint.position;
        }

        GameObject lifeBar = Instantiate(lifeBarPrefab, enemy.transform);

        RectTransform lifeBarRect = lifeBar.GetComponent<RectTransform>();
        if (lifeBarRect != null)
        {
            lifeBarRect.localPosition = new Vector3(0, -0.3f, 0);
        }
    }
}
