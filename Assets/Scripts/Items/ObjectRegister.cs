using CookieUtils;
using System;
using UnityEngine;

public enum ThrowType
{
    Throw,
	Bounce,
	Roll
}

[Serializable]
public class Throwable : MonoBehaviour
{
	[field: SerializeField]
	public GameObject Asset { get; set; }

	[field: SerializeField]
	public ThrowType ThrowType { get; set; }
}

[CreateAssetMenu(fileName = "Object Register", menuName = "Object Register")]
public class ObjectRegister : ScriptableObject
{
	[field:SerializeField]
	public Throwable[] Throwables { get; set; }

	public Throwable GetRandomThrowable()
	{
		return Throwables.RandomElement();
	}
}