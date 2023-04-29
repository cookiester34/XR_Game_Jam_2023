using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class AiController : BaseController
{
	[SerializeField]
	private ItemSpawning spawnPoints;

	private ItemCollectionPoint collectionPointGoTo;

	private TimerData AiJumpTimer;

	private void Awake()
	{
		AiJumpTimer = Utils.CreateTimer(0.5f, true);
		AiJumpTimer.EndTimer();
	}

	private void Update()
	{
		base.Update();

		// If no item
			// If being attacked
				// Wait a random/difficulty time
				// Dodge that shit
			// If not
				// Find nearest item
				// Pick it up
		// If yes item
			// Approach player until close enough (depends on item.traversableDistance)
			// Wait a random/difficulty time
			// Throw

		if (currentItem == null)
		{
			if (AiJumpTimer.timerDone.value && (shouldDodgeFromLeft || shouldDodgeFromRight))
			{
				Jump();
				AiJumpTimer.ResetTimer();
			}
			else if (collectionPointGoTo == null)
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
						//Move(goDirection);
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
			if (shouldDodgeFromLeft)
			{
				Move(Directionality.Right);
			}
			else if (shouldDodgeFromRight)
			{
				Move(Directionality.Left);
			}
			else if (facingDirection == Directionality.Left)
			{
				if (opponentPosition.GetVector3().x - currentItem.ThrowDistance > transform.position.x)
				{
					//Move(Directionality.Left);
				}
				else
				{
					ThrowItem();
				}
			}
			else
			{
				if (opponentPosition.GetVector3().x + currentItem.ThrowDistance < transform.position.x)
				{
					//Move(Directionality.Right);
				}
				else
				{
					ThrowItem();
				}
			}
		}
	}

	private void FindCollectionPoint()
	{
		collectionPointGoTo = null;
		nearbyCollectionPoint = null;
		ItemCollectionPoint destination = null;
		foreach (var window in spawnPoints.windowPersons)
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
	}
}