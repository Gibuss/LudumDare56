using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float speed = 10f;
    public int damage = 2;
    private Transform target;

    [SerializeField] private float hitRadius = 0.5f;

    public void SetTarget(Transform enemy)
    {
        target = enemy;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject);
            return;
        }

        Vector2 direction = (Vector2)(target.position - transform.position).normalized;
        transform.Translate(direction * speed * Time.deltaTime, Space.World);

        if (Vector2.Distance(transform.position, target.position) < hitRadius)
        {
            HitTarget();
        }
    }

    private void HitTarget()
    {
        enemyLife enemyScript = target.GetComponent<enemyLife>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage);
        }

        Destroy(gameObject);
    }
}
