using System;
using CookieUtils.UtilSubHelpers.DataTypes;
using CookieUtils;
using UnityEngine;

public enum Directionality
{
    Left = 1,
    Right = -1
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
    protected float health;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    protected bool isNearItemSpawnPoint = false;

    protected Throwable currentItem = null; // currentItem = revolver;

    public void Move(Directionality direction)
    {
        transform.position = new Vector3(transform.position.x + (float)direction * movementSpeed, transform.position.y, 0);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, 4);
    }

    private void Start()
    {
        currentDirection = startingDirection;
    }

    protected void Update()
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
        if (!IsGrounded())
        {
            rigidbody.AddForce(Vector3.down * Time.deltaTime * 491f, ForceMode.VelocityChange);
        }
    }
}
