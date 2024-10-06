using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

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
    private Coroutine damageCoroutine;

    private void Update()
    {
        if (target == null)
        {
            FindTarget();
        }
        else
        {
            FlipTowardsTarget();

            if (!CheckTargetIsInRange())
            {
                StopDamageCoroutine();
                target = null;
            }
        }
    }

    private void FindTarget()
    {
        RaycastHit2D[] hits = Physics2D.CircleCastAll(transform.position, targetingRange, Vector2.zero, 0f, enemyMask);

        if (hits.Length > 0)
        {
            target = hits[0].transform;
            StartDamageCoroutine();
        }
        else
        {
            StopDamageCoroutine();
        }
    }

    private bool CheckTargetIsInRange()
    {
        return Vector2.Distance(target.position, transform.position) <= targetingRange;
    }

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

    private void StartDamageCoroutine()
    {
        if (damageCoroutine == null)
        {
            damageCoroutine = StartCoroutine(InflictDamage());
        }
    }

    private IEnumerator InflictDamage()
    {
        while (target != null)
        {
            if (CheckTargetIsInRange())
            {
                if (target.TryGetComponent<enemyLife>(out enemyLife enemy))
                {
                    enemy.TakeDamage(damage);
                }
            }
            else
            {
                break;
            }

            yield return new WaitForSeconds(damageInterval);
        }

        damageCoroutine = null;
    }

    private void StopDamageCoroutine()
    {
        if (damageCoroutine != null)
        {
            StopCoroutine(damageCoroutine);
            damageCoroutine = null;
        }
    }

    private void OnDrawGizmos()
    {
        if (showGizmos)
        {
            Handles.color = Color.cyan;
            Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
        }
    }
}
