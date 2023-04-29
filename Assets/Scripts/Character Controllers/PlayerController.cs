using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : BaseController
{
    public bool isMenuCrab;

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
            ThrowItem();
        }
        else if (nearbyItem != null)
        {
            nearbyItem.PickUp();
            currentItem = nearbyItem;
            animator.SetBool("IsHolding", true);
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
        var x = (int)inputX;
        animator.SetInteger("MovementDirection", x);

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