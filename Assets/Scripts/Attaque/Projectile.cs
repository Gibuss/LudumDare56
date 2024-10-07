using UnityEngine;

public class Projectile : MonoBehaviour
{
    private Transform target;
    private int damage;
    private float speed = 10f;

    public void SetTarget(Transform target)
    {
        this.target = target;
    }

    public void SetDamage(int damage)
    {
        this.damage = damage;
    }

    private void Update()
    {
        if (target == null)
        {
            Destroy(gameObject); 
            return;
        }

        Vector2 direction = (Vector2)(target.position - transform.position);
        float distanceThisFrame = speed * Time.deltaTime;

        if (direction.magnitude <= distanceThisFrame)
        {
            HitTarget();
        }
        else
        {
            transform.Translate(direction.normalized * distanceThisFrame, Space.World);
        }
    }

    private void HitTarget()
    {
        enemyLife enemy = target.GetComponent<enemyLife>();
        if (enemy != null)
        {
            enemy.TakeDamage(damage); 
        }
        Destroy(gameObject); 
    }
}
