using Fusion;
using UnityEngine;

public class PlayerController : NetworkBehaviour
{
    [Header("Movement Settings")]
    public float moveSpeed = 6f;

    private CharacterController controller;
    private Vector2 moveInput;

    private void Awake()
    {
        controller = GetComponent<CharacterController>();
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
            Vector3 direction = new Vector3(moveInput.x, 0, moveInput.y);

            if (direction.sqrMagnitude > 0.001f)
            {
                controller.Move(direction * moveSpeed * Runner.DeltaTime);
            }
        }
    }
}

// Struct sent across the network each tick
public struct PlayerInputData : INetworkInput
{
    public Vector2 move;
}
