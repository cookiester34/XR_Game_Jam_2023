using System;
using UnityEngine.UIElements;

namespace CookieJar.Runtime.BasicUi
{
	public abstract class ElementBase<T> : VisualElement
	{
		internal Label label;
		internal T elementValue;

		public Action<T> OnToggleValueChanged;

		public string Text { get => label.text; set => label.text = value; }
		public virtual T Value { get => elementValue; set => elementValue = value; }
	}
}