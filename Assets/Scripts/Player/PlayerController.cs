using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f; // This will be replaced by PlayerStats

    private CharacterController controller;
    private Vector2 moveInput;

    private PlayerStats stats;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
    }

    public override void Spawned()
    {
        // Safe to grab networked components here
        stats = GetComponent<PlayerStats>();
    }

    public override void FixedUpdateNetwork()
    {
        // Pull input that was collected by NetworkCallbacks.OnInput
        if (GetInput(out PlayerInputData data))
        {
            moveInput = data.move;
        }

        // Only the State Authority simulates movement
        if (Object.HasStateAuthority)
        {
            float speed = stats != null ? stats.FinalMoveSpeed : moveSpeed;

            Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

            if (direction.sqrMagnitude > 0.001f)
            {
                controller.Move(direction * speed * Runner.DeltaTime);
            }
        }
    }
}

// Struct sent across the network each tick
public struct PlayerInputData : INetworkInput
{
    public Vector2 move;
    public NetworkBool attackPressed;
}
