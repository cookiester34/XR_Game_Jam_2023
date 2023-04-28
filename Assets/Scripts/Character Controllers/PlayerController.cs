using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    private PlayerInput playerInput;
    private InputAction moveAction;
    private InputAction jumpthrowpickupAction;

    private void Awake()
    {
        playerInput = GetComponent<PlayerInput>();

        if (playerInput != null)
        {
            moveAction = playerInput.actions["Move"];
            jumpthrowpickupAction = playerInput.actions["JumpOrThrowOrPickUp"];
            jumpthrowpickupAction.performed += PerformedJumpOrThrowOrPickUp;
        }
    }

    private void PerformedJumpOrThrowOrPickUp(InputAction.CallbackContext context)
    {
        if (currentItem != null)
        {
            // Throw();
        }
        else if (isNearItemSpawnPoint)
        {
            // Pick up item
        }
        else
        {
            Jump();
        }
    }

    private void Update()
    {
        base.Update();

        var inputX = moveAction.ReadValue<Vector2>().x;

        switch (inputX)
        {
            case < 0:
                Move(Directionality.Left);
                break;
            case > 0:
                Move(Directionality.Right);
                break;
        }
    }
}
