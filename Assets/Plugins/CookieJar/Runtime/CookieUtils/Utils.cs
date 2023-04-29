using CookieUtils.UtilSubHelpers;
using CookieUtils.UtilSubHelpers.DataTypes;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace CookieUtils
{
	public static class Utils
	{
		public static readonly List<TimerData> timerDatas = new();
		private static MonoUtils RefMonoUtils { get; set; }

        /// <summary>
        ///     Creates the MonoUtils class if there isn't one in the scene.
        /// </summary>
        public static void CreateMonoHelper()
		{
			if (RefMonoUtils != null) return;
			var utils = new GameObject("MonoHelper");
			RefMonoUtils = utils.AddComponent<MonoUtils>();
			Debug.Log("creating MonoUtils GameObject");
		}

		public static float[] PushArray(this float[] array, float value)
		{
			for (var i = 0; i < array.Length - 2; i++) array[i] = array[i + 1];

			array[array.Length - 1] = value;

			return array;
		}

		#region object creation

        /// <summary>
        ///     Spawn GameObject at (0,0,0), without parent.
        /// </summary>
        /// <param name="go">GameObject</param>
        public static GameObject Spawn(GameObject go)
		{
			return Object.Instantiate(go, Vector3.zero, Quaternion.identity);
		}

        /// <summary>
        ///     Spawn GameObject at position set, without parent.
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="pos">Position</param>
        public static GameObject Spawn(GameObject go, Vector3 pos)
		{
			return Object.Instantiate(go, pos, Quaternion.identity);
		}

        /// <summary>
        ///     Spawn GameObject at position set, will destroy object after time.
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="pos">Position</param>
        /// <param name="time">lifespan of object</param>
        public static GameObject Spawn(GameObject go, Vector3 pos, float time)
		{
			var tempObject = Object.Instantiate(go, pos, Quaternion.identity);
			RefMonoUtils.DestroyOb(tempObject, time);
			return tempObject;
		}

        /// <summary>
        ///     Spawn GameObject at position set, with set parent.
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="pos">Position</param>
        /// <param name="parent">Parent</param>
        public static GameObject Spawn(GameObject go, Vector3 pos, Transform parent)
		{
			return Object.Instantiate(go, pos, Quaternion.identity, parent);
		}

        /// <summary>
        ///     Spawn GameObject at position set, with set parent, will destroy object after time.
        /// </summary>
        /// <param name="go">GameObject</param>
        /// <param name="pos">Position</param>
        /// <param name="parent">Parent</param>
        /// <param name="time">lifespan of object</param>
        public static GameObject Spawn(GameObject go, Vector3 pos, Transform parent, float time)
		{
			var tempObject = Object.Instantiate(go, pos, Quaternion.identity, parent);
			RefMonoUtils.DestroyOb(tempObject, time);
			return tempObject;
		}

        /// <summary>
        ///     Set the rotation of a gameobject, using a vector3
        /// </summary>
        /// <param name="g"></param>
        /// <param name="rotation"></param>
        /// <returns></returns>
        public static void SetRotation(this GameObject g, Vector3 rotation)
		{
			g.transform.rotation = Quaternion.Euler(rotation);
		}

        /// <summary>
        ///     set the rotation of a 2D gameobject using a float for the z axis
        /// </summary>
        /// <param name="g"></param>
        /// <param name="zRotation"></param>
        /// <returns></returns>
        public static void SetRotation2D(this GameObject g, float zRotation)
		{
			g.transform.rotation = Quaternion.Euler(new Vector3(0, 0, zRotation));
		}

		#endregion

		#region Timer

        /// <summary>
        ///     initialize timer settings
        ///     - Set timer start time
        /// </summary>
        /// <param name="maxTime">max time of timer</param>
        public static TimerData CreateTimer(float maxTime)
		{
			CreateMonoHelper();
			var timer = ScriptableObject.CreateInstance<TimerData>();
			timer.InitializeTimer(maxTime);
			return timer;
		}

        /// <summary>
        ///     initialize timer settings
        ///     - Set timer start time
        ///     - If it starts of paused
        /// </summary>
        /// <param name="maxTime">max time of timer</param>
        /// <param name="paused">whether timer is paused on creation</param>
        public static TimerData CreateTimer(float maxTime, bool paused)
		{
			CreateMonoHelper();
			var timer = ScriptableObject.CreateInstance<TimerData>();
			timer.InitializeTimer(maxTime, paused);
			return timer;
		}

		public static void DestroyTimer(this TimerData timer)
		{
			timerDatas.Remove(timer);
			RefMonoUtils.DestroyOb(timer);
		}

		#endregion

		#region ScriptableData

        /// <summary>
        ///     Creates a new IntData scriptable object
        /// </summary>
        /// <returns></returns>
        public static IntData NewIntData()
		{
			return ScriptableObject.CreateInstance<IntData>();
		}

        /// <summary>
        ///     Creates a new IntData scriptable object
        /// </summary>
        /// <returns></returns>
        public static FloatData NewFloatData()
		{
			return ScriptableObject.CreateInstance<FloatData>();
		}

        /// <summary>
        ///     Creates a new IntData scriptable object
        /// </summary>
        /// <returns></returns>
        public static BoolData NewBoolData()
		{
			return ScriptableObject.CreateInstance<BoolData>();
		}

		#endregion

		#region Lists

        /// <summary>
        ///     Quick shuffle of a list
        /// </summary>
        /// <typeparam name="T">the type of the list</typeparam>
        /// <param name="list">the list to shuffle</param>
        public static void Shuffle<T>(this IList<T> list)
		{
			var n = list.Count;
			while (n > 1)
			{
				n--;
				var k = Random.Range(0, n + 1);
				(list[k], list[n]) = (list[n], list[k]);
			}
		}

        /// <summary>
        ///     Takes a list, creates a copy and returns the newly shuffled list
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="inList"></param>
        public static List<T> RandomisedCopy<T>(this IEnumerable<T> inList)
		{
			var list = new List<T>(inList);
			list.Shuffle();
			return list;
		}

        /// <summary>
        ///     Randomly picks one elements from the enumerable
        /// </summary>
        /// <typeparam name="T">The type of the item</typeparam>
        /// <param name="items">The items to random from</param>
        /// <returns></returns>
        public static T RandomElement<T>(this IEnumerable<T> items)
		{
			if (items == null)
				throw new ArgumentException("Cannot randomly pick an item from the list, the list is null!");
			var enumerable = items.ToList();
			if (!enumerable.Any())
				throw new ArgumentException(
					"Cannot randomly pick an item from the list, there are no items in the list!");
			var r = Random.Range(0, enumerable.Count());
			return enumerable.ElementAt(r);
		}

        /// <summary>
        ///     Randomly picks one element from the enumerable, taking into account a weight
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sequence"></param>
        /// <param name="weightSelector"></param>
        /// <returns></returns>
        public static T WeightedRandomElement<T>(this IEnumerable<T> sequence, Func<T, float> weightSelector)
		{
			var enumerable = sequence.ToList();
			var totalWeight = enumerable.Sum(weightSelector);
			// The weight we are after...
			var itemWeightIndex = Random.value * totalWeight;
			float currentWeightIndex = 0;
			foreach (var item in from weightedItem in enumerable
			         select new { Value = weightedItem, Weight = weightSelector(weightedItem) })
			{
				currentWeightIndex += item.Weight;

				// If we've hit or passed the weight we are after for this item then it's the one we want....
				if (currentWeightIndex >= itemWeightIndex) return item.Value;
			}

			return default;
		}

		#endregion

		#region vectors

        /// <summary>
        ///     Simple method to set the x value on the Vector3, returns a new vector3
        /// </summary>
        /// <param name="v">the vector</param>
        /// <param name="x">the x value to set</param>
        public static void SetX(this Vector3 v, float x)
		{
			v.x = x;
		}

        /// <summary>
        ///     Simple method to set the y value on the Vector3, returns a new vector3
        /// </summary>
        /// <param name="v">the vector</param>
        /// <param name="y">the y value to set</param>
        public static void SetY(this Vector3 v, float y)
		{
			v.y = y;
		}

        /// <summary>
        ///     Simple method to set the z value on the Vector3, returns a new vector3
        /// </summary>
        /// <param name="v">the vector</param>
        /// <param name="z">the z value to set</param>
        public static void SetZ(this Vector3 v, float z)
		{
			v.z = z;
		}

        /// <summary>
        ///     Simple method to turn a v2 into a v3
        /// </summary>
        /// <param name="v">The vector to convert</param>
        /// <returns></returns>
        public static Vector3 ToVector3(this Vector2 v)
		{
			return new Vector3(v.x, v.y, 0);
		}

        /// <summary>
        ///     Simple method to turn a v3 into a v2
        /// </summary>
        /// <param name="v">The vector to convert</param>
        /// <returns></returns>
        public static Vector2 ToVector2(this Vector3 v)
		{
			return new Vector2(v.x, v.y);
		}

		#endregion

		#region Objects

        /// <summary>
        ///     Destroys all the children of a given gameobject
        /// </summary>
        /// <param name="obj">The parent game object</param>
        public static void DestroyAllChildrenImmediately(this GameObject obj)
		{
			DestroyAllChildrenImmediately(obj.transform);
		}

        /// <summary>
        ///     Destroys all the children of a given transform
        /// </summary>
        /// <param name="trans">The parent transform</param>
        public static void DestroyAllChildrenImmediately(this Transform trans)
		{
			while (trans.childCount != 0)
				Object.DestroyImmediate(trans.GetChild(0).gameObject);
		}

        /// <summary>
        ///     Focuses the camera on a point in 2D space (just transforms the x and y to match the target)
        /// </summary>
        /// <param name="camera"></param>
        /// <param name="target"></param>
        public static void FocusOn2D(this Camera camera, GameObject target)
		{
			var localPosition = target.transform.localPosition;
			if (Camera.main is { })
				camera.transform.position =
					new Vector3(localPosition.x, localPosition.y, Camera.main.transform.position.z);
		}

        /// <summary>
        ///     A shorter way of testing if a game object has a component
        /// </summary>
        /// <typeparam name="T">Component type</typeparam>
        /// <param name="obj">The object to check on</param>
        /// <returns></returns>
        public static bool Has<T>(this GameObject obj) where T : Component
		{
			return obj.GetComponent<T>() != null;
		}

		#endregion

		#region Lerping

		public static Vector3 BasicLerp(Vector3 start, Vector3 end, float t)
		{
			return Vector3.Lerp(start, end, t);
		}

		public static Vector3 EaseOut(Vector3 start, Vector3 end, float t)
		{
			t = Mathf.Sin(t * Mathf.PI * 0.5f);
			return Vector3.Lerp(start, end, t);
		}

		public static Vector3 EaseIn(Vector3 start, Vector3 end, float t)
		{
			t = 1f - Mathf.Cos(t * Mathf.PI * 0.5f);
			return Vector3.Lerp(start, end, t);
		}

		public static Vector3 Smoothstep(Vector3 start, Vector3 end, float t)
		{
			t = t * t * (3f - 2f * t);
			return Vector3.Lerp(start, end, t);
		}

		public static Vector3 Smootherstep(Vector3 start, Vector3 end, float t)
		{
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			return Vector3.Lerp(start, end, t);
		}

		public static float Smoothstep(float start, float end, float t)
		{
			t = t * t * (3f - 2f * t);
			return Mathf.Lerp(start, end, t);
		}

		public static float Smootherstep(float start, float end, float t)
		{
			t = t * t * t * (t * (6f * t - 15f) + 10f);
			return Mathf.Lerp(start, end, t);
		}

        /// <summary>
        ///     Provides a framerate-independent t for lerping towards a target.
        ///     Example:
        ///     currentValue = Mathf.Lerp(currentValue, 1f, MathHelper.EasedLerpFactor(0.75f);
        ///     will cover 75% of the remaining distance between currentValue and 1 each second.
        ///     There are essentially two ways of lerping a value over time: linear (constant speed) or
        ///     eased (e.g. getting slower the closer you are to the target, see http://easings.net.)
        ///     For linear lerping (and most of the easing functions), you need to track the start and end
        ///     positions and the time that elapsed.
        ///     Calling something like
        ///     currentValue = Mathf.Lerp(currentValue, 1f, 0.95f);
        ///     every frame provides an easy way of eased lerping without tracking elapsed time or the
        ///     starting value, but since it's called every frame, the actual traversed distance per
        ///     second changes the higher the framerate is.
        ///     This function replaces the lerp T to make it framerate-independent and easier to estimate.
        ///     For more info, see https://www.scirra.com/blog/ashley/17/using-lerp-with-delta-time.
        /// </summary>
        /// <param name="factor">How much % the lerp should cover per second.</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The framerate-independent lerp t.</returns>
        public static float EasedLerpFactor(float factor, float deltaTime = 0f)
		{
			if (deltaTime == 0f)
				deltaTime = Time.deltaTime;

			return 1 - Mathf.Pow(1 - factor, deltaTime);
		}

        /// <summary>
        ///     Framerate-independent eased lerping to a target value, slowing down the closer it is.
        ///     If you call
        ///     currentValue = MathHelper.EasedLerp(currentValue, 1f, 0.75f);
        ///     each frame (e.g. in Update()), starting with a currentValue of 0, then after 1 second
        ///     it will be approximately 0.75 - which is 75% of the way between 0 and 1.
        ///     Adjusting the target or the percentPerSecond between calls is also possible.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="percentPerSecond">How much of the distance between current and target should be covered per second?</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The interpolated value from current to target.</returns>
        public static float EasedLerp(float current, float target, float percentPerSecond, float deltaTime = 0f)
		{
			var t = EasedLerpFactor(percentPerSecond, deltaTime);
			return Mathf.Lerp(current, target, t);
		}

        /// <summary>
        ///     Framerate-independent eased lerping to a target value, slowing down the closer it is.
        ///     If you call
        ///     currentValue = UnityHelper.EasedLerpVector3(currentValue, Vector2.one, 0.75f);
        ///     each frame (e.g. in Update()), starting with a currentValue of Vector2.zero, then after 1 second
        ///     it will be approximately (0.75|0.75) - which is 75% of the way between Vector2.zero and Vector2.one.
        ///     Adjusting the target or the percentPerSecond between calls is also possible.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="percentPerSecond">How much of the distance between current and target should be covered per second?</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The interpolated value from current to target.</returns>
        public static Vector2 EasedLerpVector2(Vector2 current, Vector2 target, float percentPerSecond,
			float deltaTime = 0f)
		{
			var t = EasedLerpFactor(percentPerSecond, deltaTime);
			return Vector2.Lerp(current, target, t);
		}

        /// <summary>
        ///     Framerate-independent eased lerping to a target value, slowing down the closer it is.
        ///     If you call
        ///     currentValue = UnityHelper.EasedLerpVector3(currentValue, Vector3.one, 0.75f);
        ///     each frame (e.g. in Update()), starting with a currentValue of Vector3.zero, then after 1 second
        ///     it will be approximately (0.75|0.75|0.75) - which is 75% of the way between Vector3.zero and Vector3.one.
        ///     Adjusting the target or the percentPerSecond between calls is also possible.
        /// </summary>
        /// <param name="current">The current value.</param>
        /// <param name="target">The target value.</param>
        /// <param name="percentPerSecond">How much of the distance between current and target should be covered per second?</param>
        /// <param name="deltaTime">How much time passed since the last call.</param>
        /// <returns>The interpolated value from current to target.</returns>
        public static Vector3 EasedLerpVector3(Vector3 current, Vector3 target, float percentPerSecond,
			float deltaTime = 0f)
		{
			var t = EasedLerpFactor(percentPerSecond, deltaTime);
			return Vector3.Lerp(current, target, t);
		}

		#endregion

		#region Random

        /// <summary>
        ///     Gets a random Vector2 of length 1 pointing in a random direction.
        /// </summary>
        public static Vector2 RandomOnUnitCircle
		{
			get
			{
				var angle = Random.Range(0f, Mathf.PI * 2);
				return new Vector2(Mathf.Cos(angle), Mathf.Sin(angle));
			}
		}

        /// <summary>
        ///     Returns -1 or 1 with equal change.
        /// </summary>
        public static int RandomSign => Random.value < 0.5f ? -1 : 1;

        /// <summary>
        ///     Returns true or false with equal chance.
        /// </summary>
        public static bool RandomBool => Random.value < 0.5f;

		#endregion

		#region JSON

		public static T[] FromJson<T>(string json)
		{
			var wrapper = JsonUtility.FromJson<Wrapper<T>>(json);
			return wrapper.Items;
		}

		public static string ToJson<T>(T[] array)
		{
			var wrapper = new Wrapper<T>();
			wrapper.Items = array;
			return JsonUtility.ToJson(wrapper);
		}

		public static string ToJson<T>(T[] array, bool prettyPrint)
		{
			var wrapper = new Wrapper<T>();
			wrapper.Items = array;
			return JsonUtility.ToJson(wrapper, prettyPrint);
		}

		[Serializable]
		private class Wrapper<T>
		{
			public T[] Items;
		}

		#endregion

		#region Debug

		public static void Print<T>(this List<T> list)
		{
			foreach (var VARIABLE in list) Debug.Log(VARIABLE);
		}

		public static void PrintError<T>(this T message)
		{
			Debug.LogError(message);
		}

		public static void PrintWarning<T>(this T message)
		{
			Debug.LogWarning(message);
		}

		#endregion
	}
}