using CookieUtils;
using System.Linq;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
	[SerializeField]
	private ObjectRegister objectRegister;

	[SerializeField]
	public WindowPerson[] windowPersons;

	[SerializeField]
	private int maxSpawnableObjects;

	public int NumberOfSpawnedAssets { get; set; }

	private void Start()
	{
		foreach (var windowPerson in windowPersons)
		{
			windowPerson.itemCollectionPoint.ItemSpawning = this;
		}
	}

	private void Update()
	{
		if (NumberOfSpawnedAssets < maxSpawnableObjects)
		{
			if (AttemptObjectSpawn())
			{
				NumberOfSpawnedAssets++;
			}
		}
	}

	private bool AttemptObjectSpawn()
	{
		var windowPerson = windowPersons.Where(person => !person.IsThrowing && !person.itemCollectionPoint.HasItem).ToList();

		if (windowPerson.Count <= 0) return false;

		windowPerson.RandomElement().SpawnItem(objectRegister.GetRandomThrowable());

		return true;
	}
}