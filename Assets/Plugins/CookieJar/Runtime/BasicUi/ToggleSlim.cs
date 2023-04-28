using UnityEngine.UIElements;
using static CookieJar.Runtime.VisualElementHelpers;

namespace CookieJar.Runtime.BasicUi
{
	public class ToggleSlim : ElementBase<bool>
	{
		public override bool Value
		{
			get => elementValue;
			set
			{
				elementValue = value;
				OnToggleValueChanged?.Invoke(value);
				if (value)
				{
					AddToClassList("toggle_on_slim");
				}
				else if (ClassListContains("toggle_on_slim"))
				{
					RemoveFromClassList("toggle_on_slim");
				}
			}
		}

		public ToggleSlim()
		{
			styleSheets.Add(GetStyleSheet("BasicStyleSheet"));
			AddToClassList("link_cursor");
			AddToClassList("toggle_slim");

			label = new Label();
			Add(label);

			var toggleContainer = new VisualElement();
			toggleContainer.RegisterCallback<ClickEvent>(_ =>
			{
				Value = !Value;
			});
			Add(toggleContainer);

			var toggleSwitch = new VisualElement();
			toggleContainer.Add(toggleSwitch);
		}
	}
}