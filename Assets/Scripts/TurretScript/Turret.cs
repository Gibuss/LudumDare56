using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
    [Header("Projectile")]
    [SerializeField] private GameObject projectilePrefab;

    [Header("References")]
    [SerializeField] private SpriteRenderer spriteRenderer;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private int damage = 2;
    [SerializeField] private float damageInterval = 1f;

    [Header("Debug")]
    [SerializeField] private bool showGizmos = true;

    private Transform target;
    private float nextDamageTime;

    private void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    private void Update()
    {
        TargetFind();
        FlipTowardsTarget();
        HandleAttack();
    }

    private void TargetFind()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (enemiesInRange.Length > 0)
        {
            if (target == null || Vector2.Distance(transform.position, target.position) > targetingRange)
            {
                target = GetClosestEnemy(enemiesInRange);
            }
        }
        else
        {
            target = null;
        }
    }

    private Transform GetClosestEnemy(Collider2D[] enemies)
    {
        Transform closestEnemy = null;
        float closestDistance = Mathf.Infinity;

        foreach (Collider2D enemy in enemies)
        {
            float distanceToEnemy = Vector2.Distance(transform.position, enemy.transform.position);
            if (distanceToEnemy < closestDistance)
            {
                closestDistance = distanceToEnemy;
                closestEnemy = enemy.transform;
            }
        }

        return closestEnemy;
    }

    private void FlipTowardsTarget()
    {
        if (target != null)
        {
            bool shouldFlip = target.position.x < transform.position.x;

            if (shouldFlip && !spriteRenderer.flipX)
            {
                spriteRenderer.flipX = true;
            }
            else if (!shouldFlip && spriteRenderer.flipX)
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    private void HandleAttack()
    {
        if (target != null)
        {
            if (Time.time >= nextDamageTime)
            {
                ShootProjectile();
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    private void ShootProjectile()
    {
        GameObject projectileInstance = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        Projectile projectileScript = projectileInstance.GetComponent<Projectile>();
        if (projectileScript != null)
        {
            projectileScript.SetTarget(target);
            projectileScript.SetDamage(damage);
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetingRange);
        }
    }

    public void SetStats(float newTargetingRange, int newDamage, float newDamageInterval)
    {
        targetingRange = newTargetingRange;
        damage = newDamage;
        damageInterval = newDamageInterval;
    }
}
