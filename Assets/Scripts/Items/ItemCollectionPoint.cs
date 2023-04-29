using System;
using Unity.Mathematics;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemCollectionPoint : MonoBehaviour
{
	public ItemSpawning ItemSpawning { get; set; }

	private Throwable item;

	public bool HasItem { get; private set; }

	public bool IsTemp { get; set; }

	public void SetItem(Throwable item)
	{
		this.item = item;
		HasItem = true;
	}

	public bool TryGetItem(out Throwable item)
	{
		item = null;
		if (this.item == null)
		{
			if (IsTemp)
			{
				Destroy(gameObject);
			}
			return false;
		}

		item = this.item;
		this.item = null;
		HasItem = false;

		if (IsTemp)
		{
			Destroy(gameObject);
		}
		else
		{
			ItemSpawning.NumberOfSpawnedAssets--;
		}
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