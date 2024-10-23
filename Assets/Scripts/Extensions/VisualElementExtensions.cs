using Common;
using UnityEngine.Localization;
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

        /// <summary>
        /// Extension method for binding localized text to a VisualElement.
        /// Binds a localized value from the localization table to the "text" property of a UI element.
        /// </summary>
        /// <param name="element">The VisualElement to which the localized text will be bound.</param>
        /// <param name="key">The localization key that corresponds to the entry in the localization table.</param>
        public static void BindLocalization(this VisualElement element, string key)
        {
            element.SetBinding(
                "text",
                new LocalizedString
                {
                    TableReference = LocalizationTables.TableReference,
                    TableEntryReference = key
                }
            );
        }
    }
}
