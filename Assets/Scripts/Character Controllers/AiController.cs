using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class AiController : BaseController
{
	[SerializeField]
	private ItemSpawning spawnPoints;


	private TimerData AiJumpTimer;

	private TimerData AiThrowTimer;

	[SerializeField]
	private Transform leftRandomPoint;
	[SerializeField]
	private Transform rightRandomPoint;
	[SerializeField]
	public Directionality randomDirection = Directionality.Right;

	public bool Active;

	private void Awake()
	{
		AiJumpTimer = Utils.CreateTimer(0.5f);
		AiJumpTimer.EndTimer();

		AiThrowTimer = Utils.CreateTimer(1.4f);
		AiThrowTimer.EndTimer();
	}

	private void FindCollectionPoint()
	{
		collectionPointGoTo = null;
		nearbyCollectionPoint = null;
		ItemCollectionPoint destination = null;
		foreach (var window in spawnPoints.windowPersonsAi)
		{
			if (window.itemCollectionPoint.HasItem)
			{
				if (destination == null)
				{
					destination = window.itemCollectionPoint;
				}
				else if (Vector3.Distance(transform.position, destination.transform.position) > Vector3.Distance(transform.position, window.itemCollectionPoint.transform.position))
				{
					destination = window.itemCollectionPoint;
				}
			}
		}
		collectionPointGoTo = destination;
		if (destination != null)
		{
			goDirection = transform.position.x > destination.transform.position.x ? Directionality.Right : Directionality.Left;
		}
	}

	private void FixedUpdate()
	{
		if (!Active || isDead) return;

		base.FixedUpdate();

		if (currentItem == null || !AiThrowTimer.timerDone.value)
		{
			Move(randomDirection);

			if (AiJumpTimer.timerDone.value && (shouldDodgeFromLeft || shouldDodgeFromRight))
			{
				Jump();
				AiJumpTimer.ResetTimer();
			}

			if (nearbyCollectionPoint != null)
			{
				PickUp();
			}

		}
		else
		{
			if (facingDirection == Directionality.Left)
			{
				if (opponentPosition.GetVector3().x - currentItem.ThrowDistance > transform.position.x)
				{
					Move(Directionality.Left);
				}
				else if (AiThrowTimer.timerDone.value)
				{
					ThrowItem();
					AiThrowTimer.ResetTimer();
					randomDirection = Directionality.Right;
				}
			}
			else
			{
				if (opponentPosition.GetVector3().x + currentItem.ThrowDistance < transform.position.x)
				{
					Move(Directionality.Right);
				}
				else if (AiThrowTimer.timerDone.value)
				{
					ThrowItem();
					AiThrowTimer.ResetTimer();
					randomDirection = Directionality.Right;
				}
			}
		}
	}
}