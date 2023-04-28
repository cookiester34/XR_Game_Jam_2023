using CookieJar.Runtime.BasicUi;
using UnityEditor;
using UnityEngine.UIElements;

namespace CookieJar.Editor
{
	public class Examples : EditorWindow
	{
		[MenuItem("Tools/CookieJar Examples")]
		private static void OpenWindow()
		{
			var window = GetWindow<Examples>();
			window.Show();
		}

		private void CreateGUI()
		{
			rootVisualElement.Add(new Label("Toggles"));
			rootVisualElement.Add(new ToggleRound());
			rootVisualElement.Add(new ToggleSlim());
			rootVisualElement.Add(new Label("Sliders"));
			rootVisualElement.Add(new SliderRound());
			rootVisualElement.Add(new Slider());
		}
	}
}