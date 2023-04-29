using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class AiController : BaseController
{
	[SerializeField]
	private ItemSpawning spawnPoints;

	private TimerData AiJumpTimer;

	private TimerData AiThrowTimer;

	private TimerData AiPickUpTimer;

	private TimerData AiRandomTimer;

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

		AiThrowTimer = Utils.CreateTimer(3f);
		AiThrowTimer.EndTimer();

		AiPickUpTimer = Utils.CreateTimer(3.5f);
		AiPickUpTimer.EndTimer();
	}

	private void FixedUpdate()
	{
		if (!Active || isDead) return;

		base.FixedUpdate();

		if (currentItem == null)
		{
			Move(randomDirection);

			if (AiJumpTimer.timerDone.value && (shouldDodgeFromLeft || shouldDodgeFromRight))
			{
				if (Random.Range(0, 10) <= 1)
				{
					Jump();
					AiJumpTimer.ResetTimer();
				}
			}

			if (AiPickUpTimer.timerDone.value && nearbyCollectionPoint != null)
			{
				var successfulPickUp = PickUp();
				if (successfulPickUp) AiPickUpTimer.ResetTimer();
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
					//Debug.Log("throwing");
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
					//Debug.Log("throwing");
					ThrowItem();
					AiThrowTimer.ResetTimer();
				}
			}
		}
	}
}