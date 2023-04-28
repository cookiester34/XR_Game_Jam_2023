using Unity.Mathematics;
using UnityEngine;

public class ItemSpawnPoint : MonoBehaviour
{
	private Throwable item;

	public bool TrySpawnItem(Throwable item)
	{
		if (this.item != null) return false;

		Instantiate(item, transform.position, quaternion.identity);
		this.item = item;

		return true;
	}

	public bool TryGetItem(out Throwable item)
	{
		item = null;
		if (this.item == null) return false;

		item = this.item;
		return true;
	}
}