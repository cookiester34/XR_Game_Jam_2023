using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemCollectionPoint : MonoBehaviour
{
	private Throwable item;

	public bool HasItem { get; private set; }

	public void SetItem(Throwable item)
	{
		this.item = item;
		HasItem = true;
	}

	public bool TryGetItem(out Throwable item)
	{
		item = null;
		if (this.item == null) return false;

		item = this.item;
		this.item = null;
		HasItem = false;
		return true;
	}

	private void Start()
	{
		var collider = GetComponent<SphereCollider>();
		collider.radius = 2f;
		collider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		var crab = other.GetComponent<BaseController>();
		if (crab != null)
		{
			crab.nearbyCollectionPoint = this;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var crab = other.GetComponent<BaseController>();
		if (crab != null)
		{
			crab.nearbyCollectionPoint = null;
		}
	}
}