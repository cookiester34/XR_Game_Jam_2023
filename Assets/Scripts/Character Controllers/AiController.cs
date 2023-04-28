using UnityEngine;
using UnityEngine.PlayerLoop;

public class AiController : BaseController
{
	private void Update()
	{
		base.Update();

		// If no item
			// If being attacked
				// Wait a random/difficulty time
				// Dodge that shit
			// If not
				// Find nearest item
				// Pick it up
		// If yes item
			// Approach player until close enough (depends on item.traversableDistance)
			// Wait a random/difficulty time
			// Throw

		if (currentItem == null)
		{
			Move(currentDirection);
		}
		else
		{
			Move(currentDirection);
		}
	}
}
