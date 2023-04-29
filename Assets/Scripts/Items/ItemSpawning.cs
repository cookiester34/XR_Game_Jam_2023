using CookieUtils;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
	public static ItemSpawning Instance { get; private set; }

	[SerializeField]
	private ObjectRegister objectRegister;

	[SerializeField]
	private PlayerController playerCrabTransform;

	[SerializeField]
	public WindowPerson[] windowPersons;

	public List<Throwable> allThrowables = new();

	[SerializeField]
	private int maxSpawnableObjects;

	[field:SerializeField]
	public int NumberOfSpawnedAssets { get; set; }

	private void Awake()
	{
		Instance ??= this;
		Utils.CreateMonoHelper();
	}

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

		playerCrabTransform.nearbyItem = null;
		foreach (var throwable in allThrowables.Where(throwable => !throwable.Launched))
		{
			if (Vector3.Distance(playerCrabTransform.transform.position, throwable.transform.position) <= 0.3f)
			{
				playerCrabTransform.nearbyItem = throwable;
				break;
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