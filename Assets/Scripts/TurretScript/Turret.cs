using System.Collections;
using UnityEngine;

public class Turret : MonoBehaviour
{
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

    private void Update()
    {
        TargetFind();
        FlipTowardsTarget();
        HandleAttack();
    }

    // Fonction pour trouver la cible la plus proche dans la range
    private void TargetFind()
    {
        Collider2D[] enemiesInRange = Physics2D.OverlapCircleAll(transform.position, targetingRange, enemyMask);

        if (enemiesInRange.Length > 0)
        {
            // Si l'ennemi actuel est hors de la zone, on en cherche un nouveau
            if (target == null || Vector2.Distance(transform.position, target.position) > targetingRange)
            {
                target = GetClosestEnemy(enemiesInRange);
            }
        }
        else
        {
            target = null; // Si aucun ennemi dans la zone, pas de cible
        }
    }

    // Trouver l'ennemi le plus proche parmi les ennemis dans la zone
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

    // Fonction pour orienter la tour vers la cible
    private void FlipTowardsTarget()
    {
        if (target != null)
        {
            if (target.position.x < transform.position.x)
            {
                spriteRenderer.flipX = true;
            }
            else
            {
                spriteRenderer.flipX = false;
            }
        }
    }

    // Gérer l'attaque contre l'ennemi
    private void HandleAttack()
    {
        if (target != null)
        {
            // Si le cooldown est terminé et que l'ennemi est toujours dans la zone
            if (Time.time >= nextDamageTime)
            {
                // Appliquer les dégâts
                DoDamage(target);
                // Set next attack time based on damage interval
                nextDamageTime = Time.time + damageInterval;
            }
        }
    }

    // Appliquer des dégâts à l'ennemi
    private void DoDamage(Transform enemy)
    {
        // On cherche le composant enemyLife sur l'ennemi pour lui infliger des dégâts
        enemyLife enemyScript = enemy.GetComponent<enemyLife>();
        if (enemyScript != null)
        {
            enemyScript.TakeDamage(damage); // Appel de la fonction TakeDamage
        }
    }

    // Dessiner la zone de portée avec Gizmos
    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Gizmos.color = Color.red;
            Gizmos.DrawWireSphere(transform.position, targetingRange);
        }
    }
}