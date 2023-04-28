using System;
using UnityEngine;
using UnityEngine.PlayerLoop;
using CookieUtils;

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

	public bool Launched { get; set; }
	private float destination;
	private Directionality direction = 0;

	public void Throw(Directionality direction)
	{
		Launched = true;
		this.direction = direction;
		destination = transform.position.x + (float)direction * ThrowDistance;
		switch (ThrowType)
		{
			case ThrowType.Roll:
				transform.position = new Vector3(transform.position.x + (float)direction * 5, -38.24215f, transform.position.z);
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
				Debug.Log("Stopped!");
			}
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
}