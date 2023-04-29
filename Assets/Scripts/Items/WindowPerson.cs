using Unity.Mathematics;
using CookieUtils;
using CookieUtils.UtilSubHelpers;
using UnityEngine;

public class WindowPerson : MonoBehaviour
{
	[SerializeField]
	public ItemCollectionPoint itemCollectionPoint;

	public bool IsThrowing { get; private set; }

	private Throwable throwable;

	private TimerData timer;

	public bool canSpawn;

	private void Start()
	{
		timer = Utils.CreateTimer(1f);
	}

	public void SpawnItem(Throwable item)
	{
		IsThrowing = true;
		throwable = Instantiate(item, transform.position, quaternion.identity);
		timer.ResetTimer();
		canSpawn = false;
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

		if (timer.timerDone.value)
		{
			canSpawn = true;
		}
	}
}