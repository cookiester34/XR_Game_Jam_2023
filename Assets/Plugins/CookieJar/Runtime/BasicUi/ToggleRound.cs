using UnityEngine.UIElements;
using static CookieJar.Runtime.VisualElementHelpers;

namespace CookieJar.Runtime.BasicUi
{
	public class ToggleRound : ElementBase<bool>
	{
		public override bool Value
		{
			get => elementValue;
			set
			{
				elementValue = value;
				OnToggleValueChanged?.Invoke(elementValue);
				if (elementValue)
				{
					AddToClassList("toggle_on");
				}
				else if (ClassListContains("toggle_on"))
				{
					RemoveFromClassList("toggle_on");
				}
			}
		}

		public ToggleRound()
		{
			styleSheets.Add(GetStyleSheet("BasicStyleSheet"));
			AddToClassList("link_cursor");
			AddToClassList("toggle_round");

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