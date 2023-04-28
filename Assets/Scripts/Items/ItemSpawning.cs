using UnityEngine;

public class ItemSpawning : MonoBehaviour
{
	[SerializeField]
	private ObjectRegister objectRegister;

	[SerializeField]
	private ItemSpawnPoint[] itemSpawnPoints;

	[SerializeField]
	private int maxSpawnableObjects;

	private int numberOfSpawnedAssets;

	private void Update()
	{
		if (numberOfSpawnedAssets < maxSpawnableObjects)
		{
			if (AttemptObjectSpawn())
			{
				maxSpawnableObjects++;
			}
		}
	}

	private bool AttemptObjectSpawn()
	{
		foreach (var spawnPoint in itemSpawnPoints)
		{
			if (spawnPoint.TrySpawnItem(objectRegister.GetRandomThrowable())) return true;
		}

		return false;
	}
}