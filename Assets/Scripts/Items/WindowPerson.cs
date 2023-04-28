using Unity.Mathematics;
using CookieUtils;
using UnityEngine;

public class WindowPerson : MonoBehaviour
{
	[SerializeField]
	public ItemCollectionPoint itemCollectionPoint;

	public bool IsThrowing { get; private set; }

	private Throwable throwable;

	public void SpawnItem(Throwable item)
	{
		IsThrowing = true;
		throwable = Instantiate(item, transform.position, quaternion.identity);
	}

	private void Update()
	{
		if (throwable != null)
		{
			throwable.transform.position = Utils.Smoothstep(throwable.transform.position, itemCollectionPoint.transform.position, 0.07f);
			var distance = Vector3.Distance(throwable.transform.position, itemCollectionPoint.transform.position);
			if (distance <= 0.1f)
			{
				itemCollectionPoint.SetItem(throwable);
				IsThrowing = false;
				throwable = null;
			}
		}
	}
}