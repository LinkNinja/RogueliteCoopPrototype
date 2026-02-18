using Fusion;
using UnityEngine;

public class PlayerCombat : NetworkBehaviour
{
    [Header("Combat Settings")]
    public float baseDamage = 10f;
    public float attackRange = 4f;
    public float attackCooldown = 0.4f;

    private float lastAttackTime;

    private PlayerStats stats;
    private Camera mainCam;

    public override void Spawned()
    {
        stats = GetComponent<PlayerStats>();
        Debug.Log($"Player spawned. InputAuthority: {Object.InputAuthority}, StateAuthority: {Object.StateAuthority}");
        mainCam = Camera.main;
    }

    public override void FixedUpdateNetwork()
    {
        //Debug.Log("PlayerCombat.FixedUpdateNetwork running.");

        if (GetInput(out PlayerInputData data))
        {
            if (data.attackPressed)
            {
                Debug.Log("PlayerCombat received attack input.");
                TryAttack();
            }
        }
    }


    private void TryAttack()
    {
        Debug.Log("TryAttack() called.");

        if (!Object.HasStateAuthority)
        {
            Debug.Log("Not StateAuthority — attack ignored.");
            return;
        }

        if (Runner.SimulationTime - lastAttackTime < attackCooldown)
        {
            Debug.Log("Attack on cooldown.");
            return;
        }

        Debug.Log("Attack allowed — performing hit detection.");

        lastAttackTime = Runner.SimulationTime;
        PerformHitDetection();
    }


    private void PerformHitDetection()
    {
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Vector3 direction = transform.forward;

        Debug.DrawRay(origin, direction * attackRange, Color.red, 0.1f);

        Debug.Log("Raycast fired.");

        if (Physics.Raycast(origin, direction, out RaycastHit hit, attackRange))
        {
            Debug.Log($"Raycast hit: {hit.collider.name}");

            EnemyHealth enemy = hit.collider.GetComponent<EnemyHealth>();
            if (enemy != null)
            {
                Debug.Log("EnemyHealth found — applying damage.");
                ApplyDamage(enemy);
            }
            else
            {
                Debug.Log("Hit something, but no EnemyHealth component.");
            }
        }
        else
        {
            Debug.Log("Raycast hit nothing.");
        }
    }


    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.red;
        Vector3 origin = transform.position + Vector3.up * 0.5f;
        Gizmos.DrawLine(origin, origin + transform.forward * attackRange);
    }


    private void ApplyDamage(EnemyHealth enemy)
    {
        float damage = baseDamage;

        bool isCrit = Random.Range(0f, 100f) < stats.FinalCritChance;

        if (isCrit)
        {
            damage *= 2f;
            Debug.Log($"CRIT! Damage dealt: {damage}");
        }
        else
        {
            Debug.Log($"Normal hit. Damage dealt: {damage}");
        }

        enemy.TakeDamage(damage);
    }

}