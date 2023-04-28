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

    public ItemCollectionPoint nearbyCollectionPoint;

    protected Throwable currentItem; // currentItem = revolver;

    public void Move(Directionality direction)
    {
        transform.position = new Vector3(transform.position.x + (float)direction * movementSpeed, transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        if (IsGrounded())
        {
            rigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }
    }

    public void PickUp()
    {
        if (!nearbyCollectionPoint.TryGetItem(out currentItem))
        {
            Debug.LogError("Didn't get item");
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

        if (currentItem != null)
        {
            currentItem.transform.position = transform.position + new Vector3(0, 10, 0);
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
