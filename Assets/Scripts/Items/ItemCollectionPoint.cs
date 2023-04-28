using Unity.Mathematics;
using UnityEngine;

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
}