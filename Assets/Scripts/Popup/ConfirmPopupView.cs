using UnityEngine.UIElements;

namespace Popup
{
    public sealed class ConfirmPopupView
    {
        public VisualElement ContentContainer { get; private set; }
        public Button ApplyButton { get; private set; }
        public Button ClearButton { get; private set; }

        public ConfirmPopupView(UIDocument uiDocument, string pathToParent)
        {
            var root = uiDocument.rootVisualElement.Q<VisualElement>(pathToParent);
            ContentContainer = root.Q<VisualElement>("content-container");
            ApplyButton = root.Q<Button>("apply-button");
            ClearButton = root.Q<Button>("clear-button");
        }
    }
}
