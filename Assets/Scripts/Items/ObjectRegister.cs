using CookieUtils;
using System;
using UnityEngine;

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