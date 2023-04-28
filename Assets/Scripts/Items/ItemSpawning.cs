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

	private int numberOfSpawnedAssets;

	private void Update()
	{
		if (numberOfSpawnedAssets < maxSpawnableObjects)
		{
			if (AttemptObjectSpawn())
			{
				numberOfSpawnedAssets++;
			}
		}
	}

	private bool AttemptObjectSpawn()
	{
		var windowPerson = windowPersons.Where(person => !person.IsThrowing).ToList();

		if (windowPerson.Count <= 0) return false;

		windowPerson.RandomElement().SpawnItem(objectRegister.GetRandomThrowable());

		return true;
	}
}