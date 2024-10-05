using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class Turret : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private Transform turretRotationPoint;
    [SerializeField] private LayerMask enemyMask;

    [Header("Attributes")]
    [SerializeField] private float targetingRange = 5f;
    [SerializeField] private int damage = 1;
    [SerializeField] private float damageInterval = 2f;

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
            RotateTowardsTarget();

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

    private void RotateTowardsTarget()
    {
        if (target != null)
        {
            float angle = Mathf.Atan2(target.position.y - transform.position.y, target.position.x - transform.position.x) * Mathf.Rad2Deg - 90f;
            Quaternion targetRotation = Quaternion.Euler(new Vector3(0f, 0f, angle));
            turretRotationPoint.rotation = targetRotation;
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

    private void OnDrawGizmosSelected()
    {
        Handles.color = Color.cyan;
        Handles.DrawWireDisc(transform.position, transform.forward, targetingRange);
    }
}
