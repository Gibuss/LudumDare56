using UnityEngine;

public class OnEnemyFinalPoint : MonoBehaviour
{
    [SerializeField] private defenderLife healthDef;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
        {
            switch (other.name)
            {
                case "enemyAnt(Clone)":
                    healthDef.TakeDamage(10);
                    Destroy(other.gameObject);
                    break;

                case "enemyTermite(Clone)":
                    healthDef.TakeDamage(20);
                    Destroy(other.gameObject);
                    break;

                case "enemyLadyBug(Clone)":
                    healthDef.TakeDamage(30);
                    Destroy(other.gameObject);
                    break;

                case "enemyBeetle(Clone)":
                    healthDef.TakeDamage(40);
                    Destroy(other.gameObject);
                    break;

                default:
                    break;
            }
        }
    }
}
