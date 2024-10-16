using UnityEngine.UIElements;

namespace Extensions
{
    public static class VisualElementExtensions
    {
        /// <summary>
        /// Sets the visibility of a UI element.
        /// If <paramref name="isVisible"/> is true, the element will be visible; otherwise, it will be hidden.
        /// </summary>
        /// <param name="element">The UI element for which to set the visibility.</param>
        /// <param name="isVisible">A flag indicating whether the element should be visible (true) or hidden (false).</param>
        public static void SetVisibility(this VisualElement element, bool isVisible)
        {
            if (isVisible)
            {
                element.style.visibility = Visibility.Visible;
                element.style.display = DisplayStyle.Flex;
            }
            else
            {
                element.style.visibility = Visibility.Hidden;
                element.style.display = DisplayStyle.None;
            }
        }

        /// <summary>
        /// Shows the UI element by setting its visibility to true.
        /// </summary>
        /// <param name="element">The UI element to show.</param>
        public static void Show(this VisualElement element) => element.SetVisibility(true);

        /// <summary>
        /// Hides the UI element by setting its visibility to false.
        /// </summary>
        /// <param name="element">The UI element to hide.</param>
        public static void Hide(this VisualElement element) => element.SetVisibility(false);
    }
}
