using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemyPath : MonoBehaviour
{
    [SerializeField] Transform[] PointsA;  
    [SerializeField] Transform[] PointsB;  
    [SerializeField] private float moveSpeed;

    private Transform[] selectedPoints;    
    private int pointsIndex;
    private Vector2 targetPosition;

    [SerializeField] private float offsetRange = 0.525f;

    // Start is called before the first frame update
    void Start()
    {
        if (Random.value < 0.5f)  
        {
            selectedPoints = PointsA;
        }
        else
        {
            selectedPoints = PointsB;
        }

        targetPosition = GetRandomPosition(selectedPoints[pointsIndex].position);
        transform.position = targetPosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (pointsIndex < selectedPoints.Length)
        {
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);

            if (Vector2.Distance(transform.position, targetPosition) < 0.1f)
            {
                pointsIndex++;

                if (pointsIndex < selectedPoints.Length)
                {
                    targetPosition = GetRandomPosition(selectedPoints[pointsIndex].position);
                }
            }
        }
    }

    private Vector2 GetRandomPosition(Vector2 originalPosition)
    {
        float randomX = Random.Range(originalPosition.x - offsetRange, originalPosition.x + offsetRange);
        float randomY = Random.Range(originalPosition.y - offsetRange, originalPosition.y + offsetRange);
        return new Vector2(randomX, randomY);
    }
}
