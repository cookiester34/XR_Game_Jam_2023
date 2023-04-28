
using UnityEngine.UIElements;
using static CookieJar.Runtime.VisualElementHelpers;

namespace CookieJar.Runtime.BasicUi
{
	public class SliderRound : ElementBase<float>
	{
		public SliderRound()
		{
			styleSheets.Add(GetStyleSheet("BasicStyleSheet"));
			AddToClassList("link_cursor");
			AddToClassList("slider_round");

			label = new Label();
			Add(label);

			var sliderContainer = new VisualElement();
			Add(sliderContainer);

			var sliderFill = new VisualElement();
			sliderContainer.Add(sliderFill);

			var sliderHandle = new VisualElement();
			sliderHandle.AddToClassList("slider_round_handle");
			sliderContainer.Add(sliderHandle);

			//handle Slider Fill to point clicked
			sliderContainer.RegisterCallback<ClickEvent>(evt =>
			{

			});
			var topMostParent = GetTopMostParent(sliderContainer);

			//handle slider handle drag
			sliderHandle.RegisterCallback<MouseDownEvent>(evt =>
			{
				var sliderHandlePosition = sliderHandle.style.left.value.value;
				var totalWidth = sliderContainer.layout.width - 20f;

				void Callback(MouseMoveEvent evt)
				{
					sliderHandlePosition += evt.mouseDelta.x;
					if (sliderHandlePosition <= 0)
					{
						sliderHandlePosition = 0;
					}
					else if (sliderHandlePosition >= totalWidth)
					{
						sliderHandlePosition = totalWidth;
					}
					sliderHandle.style.left = sliderHandlePosition;
				}
				topMostParent.RegisterCallback<MouseMoveEvent>(Callback);
				topMostParent.RegisterCallback<MouseUpEvent>(_ =>
				{
					topMostParent.UnregisterCallback<MouseMoveEvent>(Callback);
				});
			});
			topMostParent.RegisterCallback<GeometryChangedEvent>(evt =>
			{
				//if the window size is 0 means it was just created, because it's impossible for it to be zero after creation
				if (evt.oldRect.width <= 0) return;

				var difference = evt.newRect.width - evt.oldRect.width;
				sliderHandle.style.left = difference + sliderHandle.style.left.value.value;
			});
		}
	}
}