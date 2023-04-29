using System;
using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public enum ThrowType
{
	Throw,
	Bounce,
	Roll
}

public class Throwable : MonoBehaviour
{
	[field: SerializeField]
	public ThrowType ThrowType { get; set; }

	[field: SerializeField]
	public float ThrowSpeed { get; set; }

	[field: SerializeField]
	public float ThrowDistance { get; set; }

	[field: SerializeField]
	public SphereCollider collider;

	[field: SerializeField]
	public CapsuleCollider collisionWarner;

	public bool Launched { get; set; }

	private bool slowlyRolling;

	private Vector3 slowRollTarget;

	private float destination;

	public Directionality direction = 0;

	private TimerData timer;

	private void Start()
	{
		collider.enabled = false;
	}

	public void Throw(Directionality direction)
	{
		collider.enabled = true;
		Launched = true;
		collisionWarner.GetComponent<CapsuleCollider>().enabled = true;
		this.direction = direction;
		destination = transform.position.x + (float)direction * ThrowDistance;
		switch (ThrowType)
		{
			case ThrowType.Roll:
				transform.position = new Vector3(transform.position.x + (float)direction * 10, -37f, transform.position.z);
				break;
			case ThrowType.Bounce:
				break;
			case ThrowType.Throw:
				break;
		}
	}

	private void Update()
	{
		if (Launched)
		{
			if (!CloseEnough())
			{
				//Debug.Log($"Pos: {transform.position.x}");
				//transform.position = new Vector3(Utils.EasedLerp(transform.position.x, destination, 0.75f), transform.position.y, transform.position.z);
				transform.position += new Vector3((float)direction * ThrowSpeed, 0, 0);
			}
			else
			{
				Launched = false;
				collider.enabled = false;
				slowlyRolling = true;
				slowRollTarget = new Vector3(transform.position.x + (float) direction * 20, -37f,0);
				timer ??= Utils.CreateTimer(1f);
			}

		}

		if (slowlyRolling)
		{
			transform.position = Utils.Smoothstep(transform.position, slowRollTarget, 0.07f);
			if (Vector3.Distance(transform.position, slowRollTarget) <= 0.1f)
			{
				slowlyRolling = false;
				collisionWarner.GetComponent<CapsuleCollider>().enabled = false;
			}
		}

		if (timer?.timerDone)
		{
			var collectionPoint = new GameObject("TempColelctionPoint");
			var itemCollectionPoint = collectionPoint.AddComponent<ItemCollectionPoint>();
			itemCollectionPoint.SetItem(this);
			Utils.Spawn(collectionPoint, transform.position, 5f);
		}
	}

	private bool CloseEnough()
	{
		if (direction == Directionality.Left)
		{
			return transform.position.x >= destination;
		}
		return transform.position.x <= destination;
	}

	private void OnTriggerEnter(Collider other)
	{
		var hitting = other.GetComponentInParent<BaseController>();
		if (hitting != null)
		{
			hitting.health -= 1;
			Debug.Log($"Health: {hitting.health}");
		}
	}
}