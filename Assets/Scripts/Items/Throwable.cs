using UnityEngine;

public enum ThrowType
{
	Throw,
	Bounce,
	Roll
}

public class Throwable : MonoBehaviour
{
	[field: SerializeField]
	public ThrowType ThrowType { get; set; }

	[field: SerializeField]
	public float ThrowDistance { get; set; }
}