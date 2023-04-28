using System;
using CookieUtils.UtilSubHelpers.DataTypes;
using UnityEngine;

public enum Directionality
{
    Left = -1,
    Right = 1
}

public class BaseController : MonoBehaviour
{
    [SerializeField]
    private Rigidbody rigidbody;

    [SerializeField]
    protected Vector3Data opponentPosition;

    [SerializeField]
    private Directionality startingDirection;

    protected Directionality currentDirection;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    protected bool isMoving = false;

    public void Move(Directionality direction)
    {
        rigidbody.AddForce((float)direction * movementSpeed * Time.deltaTime, 0, 0);
        rigidbody.velocity = new Vector3(Mathf.Clamp(rigidbody.velocity.x, 0, movementSpeed), rigidbody.velocity.y, 0);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(0, jumpForce * Time.deltaTime, 0);
        }
    }

    private void ApplyFriction()
    {
        rigidbody.velocity = new Vector3(Mathf.Lerp(rigidbody.velocity.x, 0f, 0.5f), rigidbody.velocity.y, 0);
    }

    private bool IsGrounded()
    {
        return transform.position.y <= 0f;
    }

    private void Start()
    {
        currentDirection = startingDirection;
    }

    private void Update()
    {
        var positionX = transform.position.x;
        var opponentX = opponentPosition.GetVector3().x;

        if (positionX > opponentX)
        {
            currentDirection = Directionality.Right;
        }
        else if (positionX < opponentX)
        {
            currentDirection = Directionality.Left;
        }
    }

    private void FixedUpdate()
    {
        if (!isMoving)
        {
            ApplyFriction();
        }
    }
}
