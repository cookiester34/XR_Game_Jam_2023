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
    protected Vector3Data myPosition;

    [SerializeField]
    protected Vector3Data opponentPosition;

    [SerializeField]
    private Directionality startingDirection;
    protected Directionality facingDirection;

    [SerializeField]
    public int health;

    [SerializeField]
    private float movementSpeed;

    [SerializeField]
    private float jumpForce;

    [SerializeField]
    protected Animator animator;

    public ItemCollectionPoint nearbyCollectionPoint;

    public Throwable nearbyItem;

    public Throwable currentItem; // currentItem = revolver;

    [field:SerializeField]
    public bool IsPlayer { get; set; }

    public bool shouldDodgeFromLeft;
    public bool shouldDodgeFromRight;

    public void Move(Directionality direction)
    {
        if (!IsPlayer)
        {
            animator.SetInteger("MovementDirection", (int) direction);
        }
        transform.position = new Vector3(transform.position.x + (float)direction * movementSpeed, transform.position.y, transform.position.z);
    }

    public void Jump()
    {
        if (IsGrounded() && transform.position.y <= -34f)
        {
            animator.SetTrigger("Jump");
            rigidbody.AddForce(0, jumpForce, 0, ForceMode.VelocityChange);
        }
    }

    public void PickUp()
    {
        if (!nearbyCollectionPoint.TryGetItem(out currentItem))
        {
            Debug.LogError("Didn't get item");
        }
        else
        {
            animator.SetBool("IsHolding", true);
        }
    }

    public void ThrowItem()
    {
        animator.SetBool("IsHolding", false);
        animator.SetTrigger("Throw");

        Invoke(nameof(TheRealThrow), 0.15f);
    }

    private void TheRealThrow()
    {
        currentItem.Throw((int) facingDirection, this);
        currentItem = null;
    }

    private bool IsGrounded()
    {
        return Physics.Raycast(transform.position + Vector3.up, Vector3.down, 1f);
    }

    private void Start()
    {
        facingDirection = startingDirection;
    }

    protected void Update()
    {

    }

    protected void FixedUpdate()
    {
        if (!IsGrounded())
        {
            rigidbody.AddForce(Vector3.down * Time.deltaTime * 491f, ForceMode.VelocityChange);
            animator.SetBool("Grounded", false);
        }
        else
        {
            animator.SetBool("Grounded", true);
        }

        var positionX = transform.position.x;
        var opponentX = opponentPosition.GetVector3().x;

        if (positionX > opponentX)
        {
            facingDirection = Directionality.Right;
        }
        else if (positionX < opponentX)
        {
            facingDirection = Directionality.Left;
        }

        if (currentItem != null)
        {
            currentItem.transform.position = transform.position + new Vector3(0, 0.15f, 0);
        }

        myPosition.SetVector3(transform.position);
    }
}