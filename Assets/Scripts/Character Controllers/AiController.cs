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

	private void Awake()
	{
		AiJumpTimer = Utils.CreateTimer(0.5f);
		AiJumpTimer.EndTimer();

		AiThrowTimer = Utils.CreateTimer(1.4f);
		AiThrowTimer.EndTimer();
	}

	private void FixedUpdate()
	{
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