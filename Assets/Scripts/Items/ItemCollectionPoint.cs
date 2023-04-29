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
		item.collectionPoint = this;
		HasItem = true;
	}

	public void RemoveItem()
	{
		item = null;
		HasItem = false;
		ItemSpawning.NumberOfSpawnedAssets--;
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
		item.PickUp();
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
		collider.radius = 4f;
		collider.isTrigger = true;
	}

	private void OnTriggerEnter(Collider other)
	{
		var crab = other.GetComponent<BaseController>();
		if (crab != null && !crab.IsPlayer)
		{
			crab.nearbyCollectionPoint = this;
		}
	}

	private void OnTriggerExit(Collider other)
	{
		var crab = other.GetComponent<BaseController>();
		if (crab != null && !crab.IsPlayer)
		{
			crab.nearbyCollectionPoint = null;
		}
	}
}