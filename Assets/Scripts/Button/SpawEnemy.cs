using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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

        //On va chercher le script emptyLife contenu dans un des enfants pour le mettre dans notre script ennemyLife
        enemyLife lifeEnemy = enemy.GetComponent<enemyLife>();
        emptylife lifeEmpty = enemy.GetComponentInChildren<emptylife>();
        lifeEnemy.lifeBarScript = lifeEmpty;


        Slider sliderLife = enemy.GetComponentInChildren<Slider>();
        sliderLife.maxValue = lifeEnemy.MaxLife;
        sliderLife.value = lifeEnemy.MaxLife;
        lifeEnemy.lifeBar = sliderLife;
    }
}
