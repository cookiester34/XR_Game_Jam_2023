using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

[RequireComponent(typeof(SphereCollider))]
public class ItemCollectionPoint : MonoBehaviour
{
	public ItemSpawning ItemSpawning { get; set; }

	private Throwable item;

	public bool HasItem { get; private set; }

	public bool IsTemp { get; set; }

	public bool canSpawn = true;

	private TimerData timer;

	private TimerData deleteItemTimer;

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
		timer.ResetTimer();
		deleteItemTimer.ResetTimer();
		canSpawn = false;
	}

	public bool TryGetItem(out Throwable item)
	{
		item = null;
		if (this.item == null)
		{
			return false;
		}

		item = this.item;
		item.PickUp();
		return true;
	}

	private void Start()
	{
		timer = Utils.CreateTimer(3f);
		deleteItemTimer = Utils.CreateTimer(9f);
		var collider = GetComponent<SphereCollider>();
		collider.radius = 0.1f;
		collider.isTrigger = true;
	}

	private void Update()
	{
		if (timer.timerDone.value)
		{
			canSpawn = true;
		}

		if (deleteItemTimer.timerDone.value)
		{
			deleteItemTimer.ResetTimer();
			timer.ResetTimer();
			canSpawn = true;

			if (item != null)
			{
				item.DestroyItem();
			}

			item = null;
			HasItem = false;
		}
	}

	public void Wipe()
	{
		canSpawn = true;

		if (item != null)
		{
			item.DestroyItem();
		}

		item = null;
		HasItem = false;
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