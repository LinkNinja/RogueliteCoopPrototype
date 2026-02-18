using Fusion;
using UnityEngine;

public class EnemyHealth : NetworkBehaviour
{
    [Header("Enemy Health")]
    [Networked] public float CurrentHP { get; set; } = 20f;

    public void TakeDamage(float amount)
    {
        CurrentHP -= amount;

        Debug.Log($"Enemy took {amount} damage. Remaining HP: {CurrentHP}");

        if (CurrentHP <= 0)
        {
            Debug.Log("Enemy died — despawning.");
            Runner.Despawn(Object);
        }
    }
}