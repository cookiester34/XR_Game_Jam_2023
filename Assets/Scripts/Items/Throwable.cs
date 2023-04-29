using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class Throwable : MonoBehaviour
{
	[SerializeField]
	private float throwSpeed;

	[SerializeField]
	private float throwDistance;
	public float ThrowDistance => throwDistance;

	[field: SerializeField]
	public CapsuleCollider collisionWarner;

	public bool Launched { get; set; }

	private Vector3 target;
	private Vector3 rollTarget;

	private int direction;

	public bool Throwing { get; set; }
	private bool rolling;
	private bool waiting;

	private TimerData timer;

	public ItemCollectionPoint collectionPoint;

	private void Start()
	{
		ItemSpawning.Instance.allThrowables.Add(this);
		timer = Utils.CreateTimer(5f, true);
	}

	public void Throw(int direction)
	{
		collisionWarner.GetComponent<CapsuleCollider>().enabled = true;
		this.direction = direction;
		transform.position += new Vector3(0, -7, 0);
		target = transform.position + new Vector3(direction * throwDistance, 0,0);
		Throwing = true;
	}

	private void Update()
	{
		if (Throwing)
		{
			transform.position += new Vector3(direction * throwSpeed, 0, 0);
			if (Vector3.Distance(transform.position, target) <= 0.2f)
			{
				Throwing = false;
				rolling = true;
				rollTarget = transform.position + new Vector3(direction * 20f, 0, 0);
			}
		}

		if (rolling)
		{
			transform.position = Utils.Smoothstep(transform.position, rollTarget, 0.07f);
			if (Vector3.Distance(transform.position, rollTarget) <= 0.2f)
			{
				rolling = false;
				waiting = true;
				timer.Unpause();
				collisionWarner.GetComponent<CapsuleCollider>().enabled = false;
			}

		}
	}

	public void PickUp()
	{
		if (collectionPoint != null)
		{
			collectionPoint.RemoveItem();
		}
		timer.Pause();
		timer.ResetTimer();
	}

	private void FixedUpdate()
	{
		if (timer.timerDone.value)
		{
			DestroyItem();
		}
	}

	private void DestroyItem()
	{
		ItemSpawning.Instance.allThrowables.Remove(this);
		Destroy(gameObject);
	}
}