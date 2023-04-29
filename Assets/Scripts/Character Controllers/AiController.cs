using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class AiController : BaseController
{
	[SerializeField]
	private ItemSpawning spawnPoints;

	private ItemCollectionPoint collectionPointGoTo;

	private Directionality goDirection = 0;

	private TimerData AiJumpTimer;

	private bool doingSomethingRandom = true;
	private TimerData AiDoingSomethingRandomTimer;

	private TimerData AiThrowTimer;

	[SerializeField]
	private Transform leftRandomPoint;
	[SerializeField]
	private Transform rightRandomPoint;

	public Directionality randomDirection = Directionality.Left;

	public bool Active;

	private void Awake()
	{
		AiJumpTimer = Utils.CreateTimer(0.5f);
		AiJumpTimer.EndTimer();

		AiDoingSomethingRandomTimer = Utils.CreateTimer(2f);
		AiDoingSomethingRandomTimer.ResetTimer();

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

		if (!doingSomethingRandom && Random.Range(0, 400) <= 1)
		{
			doingSomethingRandom = true;
			AiDoingSomethingRandomTimer.ResetTimer();
			Debug.Log("started being random");
		}

		if (doingSomethingRandom)
		{
			if (!AiDoingSomethingRandomTimer.timerDone.value)
			{
				// Move(Random.Range(0, 10) <= 9 ? facingDirection : facingDirection == Directionality.Left ? Directionality.Right : Directionality.Left);
				if (Vector3.Distance(transform.position, leftRandomPoint.position) <= 0.5f)
				{
					randomDirection = Directionality.Right;
					Move(randomDirection);
				}
				else if (Vector3.Distance(transform.position, rightRandomPoint.position) <= 0.5f)
				{
					randomDirection = Directionality.Left;
					Move(randomDirection);
				}
			}
			else
			{
				doingSomethingRandom = false;
				Debug.Log("stopped being random");
			}
		}
		else if (currentItem == null)
		{
			if (AiJumpTimer.timerDone.value && (shouldDodgeFromLeft || shouldDodgeFromRight))
			{
				Jump();
				AiJumpTimer.ResetTimer();
			}
			if (collectionPointGoTo == null)
			{
				FindCollectionPoint();
			}
			else
			{
				if (nearbyCollectionPoint != null)
				{
					PickUp();
					collectionPointGoTo = null;
				}
				else if (collectionPointGoTo != null)
				{
					if (collectionPointGoTo.HasItem)
					{
						Move(goDirection);
					}
					else
					{
						collectionPointGoTo = null;
					}
				}
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
				}
			}
		}
	}
}