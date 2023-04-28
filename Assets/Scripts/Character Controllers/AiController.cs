using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class AiController : BaseController
{
	[SerializeField]
	private ItemSpawning spawnPoints;

	private ItemCollectionPoint collectionPointGoTo;
	private Directionality goDirection;

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
			if (collectionPointGoTo == null)
			{
				FindCollectionPoint();
			}
			else
			{
				if (nearbyCollectionPoint != null)
				{
					PickUp();
				}
				else if (collectionPointGoTo != null)
				{
					Move(goDirection);
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
				else
				{
					ThrowItem();
				}
			}
			else
			{
				if (opponentPosition.GetVector3().x + currentItem.ThrowDistance < transform.position.x)
				{
					Move(Directionality.Right);
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
		goDirection = transform.position.x > destination.transform.position.x ? Directionality.Right : Directionality.Left;
		Debug.Log($"Destination Direction: {goDirection}");
	}
}
