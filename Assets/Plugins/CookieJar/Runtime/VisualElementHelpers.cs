using System.Linq;
using UnityEditor;
using UnityEngine.UIElements;

namespace CookieJar.Runtime
{
	public static class VisualElementHelpers
	{
		public static StyleSheet GetStyleSheet(string name)
		{
#if UNITY_EDITOR
			return AssetDatabase.LoadAssetAtPath<StyleSheet>(
				AssetDatabase.GUIDToAssetPath(AssetDatabase.FindAssets(name).First()));
#else
			return null;
#endif
		}

		public static VisualElement GetTopMostParent(VisualElement element)
		{
			var parent = element.parent;
			var topLevelFound = false;
			while (!topLevelFound)
			{
				var elementParent = parent.parent;
				if (elementParent != null)
				{
					parent = elementParent;
				}
				else
				{
					topLevelFound = true;
				}
			}
			return parent;
		}
	}
}